using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using EGS.Stark.Booking.HttpService.Helper;
using EGS.Stark.Booking.Manager;
using EGS.Stark.Booking.Model;
using EGS.Stark.Web;
using EGS.Stark.Web.ApiResults;

namespace EGS.Stark.Booking.HttpService.Controllers
{
    [RoutePrefix("books")]
    public class BookController : Web.ApiController
    {
        private readonly IBookManager _bookManager;
        private readonly IBookItemManager _bookItemManager;

        public BookController(IBookManager bookManager, IBookItemManager bookItemManager)
        {
            _bookManager = bookManager;
            _bookItemManager = bookItemManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(int id, CancellationToken cancellationToken)
        {
            return await Ok(new ApiResultModel<Book>()
            {
                Data = await _bookManager.GetAsyncById(id)
            }).ExecuteAsync(cancellationToken);
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> SearchAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            int skip = (parameters.ReadPageNumber() - 1) * (parameters.ReadPageSize());
            Func<Book, bool> func = ExpressionBuilder.And<Book>(parameters);
            IQueryable<Book> result = _bookManager.GetAllFiltering(func);

            IQueryable<Book> orderedResult = SortBuilder.SortQueries(parameters.ReadSortParameters(), result);

            return await Ok(new ApiResultModel<IEnumerable<Book>>
            {
                Data = orderedResult.Skip(skip).Take(parameters.ReadPageSize()),
                Pagination = new ApiResultPagination(parameters.ReadPageNumber(), orderedResult.Count(), parameters.ReadPageSize()),
            }).ExecuteAsync(cancellationToken);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> AddAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            var userId = parameters.ReadOptional<int>("UserId");
            var token = parameters.ReadRequired<string>("Token");
            var email = parameters.ReadOptional<string>("Email");
            var merchantId = parameters.ReadRequired<int>("MerchantId");
            var purposeId = parameters.ReadRequired<int>("PurposeId");
            var currencyId = parameters.ReadRequired<int>("CurrencyId");
            var paidAmount = parameters.ReadOptional<decimal>("PaidAmount");
            var amount = parameters.ReadOptional<decimal>("amount");
            var discountAmount = parameters.ReadOptional<decimal>("DiscountAmount");
            var additionalCostAmount = parameters.ReadOptional<decimal>("AdditionalCostAmount");
            var statusId = parameters.ReadOptional<int>("StatusId");
            var phone = parameters.ReadOptional<string>("Phone");
            var extraRequest = parameters.ReadOptional<string>("ExtraRequest");
            var expirationDateTime = parameters.ReadOptional<DateTime>("ExpirationDateTime");


            var book = new Book()
            {
                UserId = userId,
                Token = token,
                Email = email,
                MerchantId = merchantId,
                PurposeId = purposeId,
                CurrencyId = currencyId,
                PaidAmount = paidAmount,
                Amount = amount,
                DiscountAmount = discountAmount,
                AdditionalCostAmount = additionalCostAmount,
                StatusId = statusId,
                Phone = phone,
                ExtraRequest = extraRequest,
                ExpirationDateTime = expirationDateTime
            };

            book =await _bookManager.AddAsync(book);

            return await Ok(new ApiResultModel<Book>
            {
                Data = book
            }).ExecuteAsync(cancellationToken);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> UpdateAsync(ApiParameters parameters, CancellationToken cancellationToken)
        {
            var id= parameters.ReadOptional<int>("Id");
            var userId = parameters.ReadOptional<int>("UserId");
            var token = parameters.ReadRequired<string>("Token");
            var email = parameters.ReadOptional<string>("Email");
            var merchantId = parameters.ReadRequired<int>("MerchantId");
            var purposeId = parameters.ReadRequired<int>("PurposeId");
            var currencyId = parameters.ReadRequired<int>("CurrencyId");
            var paidAmount = parameters.ReadOptional<decimal>("PaidAmount");
            var amount = parameters.ReadOptional<decimal>("amount");
            var discountAmount = parameters.ReadOptional<decimal>("DiscountAmount");
            var additionalCostAmount = parameters.ReadOptional<decimal>("AdditionalCostAmount");
            var statusId = parameters.ReadOptional<int>("StatusId");
            var phone = parameters.ReadOptional<string>("Phone");
            var extraRequest = parameters.ReadOptional<string>("ExtraRequest");
            var expirationDateTime = parameters.ReadOptional<DateTime>("ExpirationDateTime");

            var book =await _bookManager.GetAsyncById(id);

                book.UserId = userId;
                book.Token = token;
                book.Email = email;
                book.MerchantId = merchantId;
                book.PurposeId = purposeId;
                book.CurrencyId = currencyId;
                book.PaidAmount = paidAmount;
                book.Amount = amount;
                book.DiscountAmount = discountAmount;
                book.AdditionalCostAmount = additionalCostAmount;
                book.StatusId = statusId;
                book.Phone = phone;
                book.ExtraRequest = extraRequest;
                book.ExpirationDateTime = expirationDateTime;
                book.UpdateDateTime=DateTime.Now;

            book = await _bookManager.UpdateAsync(book);

            return await Ok(new ApiResultModel<Book>
            {
                Data = book
            }).ExecuteAsync(cancellationToken);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
           var book=await _bookManager.GetAsyncById(id);
            book.IsActive = false;
            book.DeleteDateTime=DateTime.Now;

            await _bookManager.UpdateAsync(book);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public async Task<HttpResponseMessage> GetBookByToken(string token, CancellationToken cancellationToken)
        {
            return await Ok(new ApiResultModel<Book>()
            {
                Data = _bookManager.GetAll(p => p.Token == token).OrderByDescending(p=>p.CreatationDateTime).FirstOrDefault()
            }).ExecuteAsync(cancellationToken);
        }

        [HttpGet]
        [Route("id")]
        public async Task<HttpResponseMessage> GetAsyncById(int id, CancellationToken cancellationToken)
        {
            return await Ok(new ApiResultModel<BookItem>()
            {
                Data = await _bookItemManager.GetAsyncById(id)
            }).ExecuteAsync(cancellationToken);
        }

        [HttpGet]
        [Route("Items")]
        public async Task<HttpResponseMessage> SearchItemAsync(ApiParameters parameters, CancellationToken cancellationToken)
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
        [Route("{id}/Items")]
        public async Task<HttpResponseMessage> AddItemAsync(int id,ApiParameters parameters, CancellationToken cancellationToken)
        {
            var serviceId = parameters.ReadRequired<int>("ServiceId");
            var bookStatus = parameters.ReadRequired<int>("BookStatus");
            var count = parameters.ReadRequired<int>("Count");
            var paidAmount = parameters.ReadRequired<int>("PaidAmount");
            var amount = parameters.ReadRequired<int>("Amount");
            var discountAmount = parameters.ReadRequired<int>("DiscountAmount");
            var additionalCostAmount = parameters.ReadRequired<int>("AdditionalCostAmount");

            var bookItem = new BookItem()
            {
                BookId = id,
                ServiceId = serviceId,
                BookStatus = bookStatus,
                Count = count,
                PaidAmount = paidAmount,
                Amount = amount,
                DiscountAmount = discountAmount,
                AdditionalCostAmount = additionalCostAmount
            };

            bookItem = await _bookItemManager.AddAsync(bookItem);

            return await Ok(new ApiResultModel<BookItem>
            {
                Data = bookItem
            }).ExecuteAsync(cancellationToken);
        }

        [HttpPut]
        [Route("{id}/Items/{itemId}")]
        public async Task<HttpResponseMessage> UpdateItemAsync(int id,int itemId,ApiParameters parameters, CancellationToken cancellationToken)
        {
            var serviceId = parameters.ReadRequired<int>("ServiceId");
            var bookStatus = parameters.ReadRequired<int>("BookStatus");
            var count = parameters.ReadRequired<int>("Count");
            var paidAmount = parameters.ReadRequired<int>("PaidAmount");
            var amount = parameters.ReadRequired<int>("Amount");
            var discountAmount = parameters.ReadRequired<int>("DiscountAmount");
            var additionalCostAmount = parameters.ReadRequired<int>("AdditionalCostAmount");

            var bookItem = await _bookItemManager.GetAsyncById(itemId);

            bookItem.BookId = id;
            bookItem.ServiceId = serviceId;
            bookItem.BookStatus = bookStatus;
            bookItem.Count = count;
            bookItem.PaidAmount = paidAmount;
            bookItem.Amount = amount;
            bookItem.DiscountAmount = discountAmount;
            bookItem.AdditionalCostAmount = additionalCostAmount;
            bookItem.UpdateDateTime = DateTime.Now;

            bookItem = await _bookItemManager.UpdateAsync(bookItem);

            return await Ok(new ApiResultModel<BookItem>
            {
                Data = bookItem
            }).ExecuteAsync(cancellationToken);
        }

        [HttpDelete]
        [Route("{id}/Items/{itemId}")]
        public async Task<HttpResponseMessage> DeleteItemAsync(int id,int itemId)
        {
            var bookItem = _bookItemManager.GetAll(p => p.Id == itemId && p.BookId == id).FirstOrDefault();
            if (bookItem != null)
            {
                bookItem.IsActive = false;
                bookItem.DeleteDateTime = DateTime.Now;

                await _bookItemManager.UpdateAsync(bookItem);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
