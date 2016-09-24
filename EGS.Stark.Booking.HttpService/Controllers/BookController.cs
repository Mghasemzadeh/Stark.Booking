using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using EGS.Stark.Booking.Manager;
using EGS.Stark.Booking.Model;
using EGS.Stark.Web.ApiResults;

namespace EGS.Stark.Booking.HttpService.Controllers
{
    [RoutePrefix("book")]
    public class BookController : Web.ApiController
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(int id, CancellationToken cancellationToken)
        {
            return await Ok(new ApiResultModel<Book>()
            {
                Data = await _bookService.GetAsyncById(id)
            }).ExecuteAsync(cancellationToken);
        }


    }
}
