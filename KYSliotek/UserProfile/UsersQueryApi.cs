using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Serilog;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    [Route("/users")]
    public class UsersQueryApi : Controller
    {
        private readonly IAsyncDocumentSession _session;
        private static ILogger _log = Log.ForContext<UsersQueryApi>();

        public UsersQueryApi(IAsyncDocumentSession session)
        {
            _session = session;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get(QueryModels.GetUsers request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);
       
        [HttpGet]//("{id}")
        public async Task<IActionResult> Get(QueryModels.GetUserById request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

    }
}
