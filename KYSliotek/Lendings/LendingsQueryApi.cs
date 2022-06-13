using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Serilog;
using System.Threading.Tasks;

namespace KYSliotek.Lendings
{
    public class LendingsQueryApi : Controller
    {
        private readonly IAsyncDocumentSession _session;
        private static ILogger _log = Log.ForContext<LendingsQueryApi>();

        public LendingsQueryApi(IAsyncDocumentSession session)
        {
            _session = session;
        }

        [HttpGet]
        [Route("list")]
        public Task<IActionResult> Get(QueryModels.GetSuccessfullLendings request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

        [HttpGet]       //GetById
        public Task<IActionResult> Get(QueryModels.GetLendingDetails request)
           => RequestHandler.HandleQuery(() => _session.Query(request), _log);

        [HttpGet]
        [Route("myLendings")]
        public Task<IActionResult> Get(QueryModels.GetUsersLendings request)
           => RequestHandler.HandleQuery(() => _session.Query(request), _log);
    }
}
