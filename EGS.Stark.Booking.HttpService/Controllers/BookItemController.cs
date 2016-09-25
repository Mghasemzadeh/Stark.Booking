using EGS.Stark.Booking.Menager;
using EGS.Stark.Web.ApiResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using EGS.Stark.Booking.HttpService.Helper;
using EGS.Stark.Booking.Model;
using EGS.Stark.Web;

namespace EGS.Stark.Booking.HttpService.Controllers
{
    [RoutePrefix("bookItems")]
    public class BookItemController : Web.ApiController
    {
        private readonly BookItemManager _bookItemManager;

        public BookItemController(BookItemManager bookItemManager)
        {
            _bookItemManager = bookItemManager;
        }

        [HttpGet]
        [Route("id")]
        public async Task<HttpResponseMessage> GetAsyncById(int id,CancellationToken cancellationToken)
        {
            return await Ok(new ApiResultModel<BookItem>()
            {
                Data = await _bookItemManager.GetAsyncById(id)
            }).ExecuteAsync(cancellationToken);
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> SearchAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            int skip = (parameters.ReadPageNumber() - 1) * (parameters.ReadPageSize());
            Func<BookItem, bool> func = ExpressionBuilder.And<BookItem>(parameters);
            IQueryable<BookItem> result = _bookItemManager.GetAllFiltering(func);

            IQueryable<BookItem> orderedResult = SortBuilder.SortQueries(parameters.ReadSortParameters(), result);

            return await Ok(new ApiResultModel<IEnumerable<BookItem>>
            {
                Data = orderedResult.Skip(skip).Take(parameters.ReadPageSize()),
                Pagination = new ApiResultPagination(parameters.ReadPageNumber(), orderedResult.Count(), parameters.ReadPageSize()),
            }).ExecuteAsync(cancellationToken);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> AddAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            var bookId = parameters.ReadRequired<int>("BookId");
            var serviceId = parameters.ReadRequired<int>("ServiceId");
            var bookStatus= parameters.ReadRequired<int>("BookStatus");
            var count = parameters.ReadRequired<int>("Count");
            var paidAmount = parameters.ReadRequired<int>("PaidAmount");
            var amount = parameters.ReadRequired<int>("Amount");
            var discountAmount = parameters.ReadRequired<int>("DiscountAmount");
            var additionalCostAmount = parameters.ReadRequired<int>("AdditionalCostAmount");

            var bookItem=new BookItem()
            {
                BookId = bookId,
                ServiceId = serviceId,
                BookStatus = bookStatus,
                Count = count,
                PaidAmount = paidAmount,
                Amount = amount,
                DiscountAmount = discountAmount,
                AdditionalCostAmount = additionalCostAmount
            };

            bookItem =await _bookItemManager.AddAsync(bookItem);

            return await Ok(new ApiResultModel<BookItem>
            {
                Data = bookItem
            }).ExecuteAsync(cancellationToken);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> UpdateAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            var id = parameters.ReadRequired<int>("id");
            var bookId = parameters.ReadRequired<int>("BookId");
            var serviceId = parameters.ReadRequired<int>("ServiceId");
            var bookStatus = parameters.ReadRequired<int>("BookStatus");
            var count = parameters.ReadRequired<int>("Count");
            var paidAmount = parameters.ReadRequired<int>("PaidAmount");
            var amount = parameters.ReadRequired<int>("Amount");
            var discountAmount = parameters.ReadRequired<int>("DiscountAmount");
            var additionalCostAmount = parameters.ReadRequired<int>("AdditionalCostAmount");

            var bookItem =await _bookItemManager.GetAsyncById(id);

            bookItem.BookId = bookId;
            bookItem.ServiceId = serviceId;
            bookItem.BookStatus = bookStatus;
            bookItem.Count = count;
            bookItem.PaidAmount = paidAmount;
            bookItem.Amount = amount;
            bookItem.DiscountAmount = discountAmount;
            bookItem.AdditionalCostAmount = additionalCostAmount;
            bookItem.UpdateDateTime=DateTime.Now;

            bookItem = await _bookItemManager.UpdateAsync(bookItem);

            return await Ok(new ApiResultModel<BookItem>
            {
                Data = bookItem
            }).ExecuteAsync(cancellationToken);
        }

        [HttpDelete]
        [Route("id")]
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var bookItem = await _bookItemManager.GetAsyncById(id);
            bookItem.IsActive = false;
            bookItem.DeleteDateTime = DateTime.Now;

            await _bookItemManager.UpdateAsync(bookItem);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
