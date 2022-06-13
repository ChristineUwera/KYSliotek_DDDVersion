using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Serilog;
using System.Threading.Tasks;

namespace KYSliotek.Books
{
    [Route("/books")]
    public class BooksQueryApi : Controller
    {
        private readonly IAsyncDocumentSession _session;
        private static ILogger _log = Log.ForContext<BooksQueryApi>();

        public BooksQueryApi(IAsyncDocumentSession session)
        {
            _session = session;
        }

        [HttpGet]
        [Route("list")]
        public Task<IActionResult> Get(QueryModels.GetPublishedBooks request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);


        [HttpGet]
        public Task<IActionResult> Get(QueryModels.GetPublicBook request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

        //    [HttpGet]
        //    [Route("borrowed")]
        //    public Task<IActionResult> Get(QueryModels.GetBorrowedBooks request)
        //        => RequestHandler.HandleQuery(() => _session.Query(request), _log);
        //}
    }
}
