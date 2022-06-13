using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace KYSliotek.Lendings
{
    [Route("/lendings")]
    public class LendingCommandsApi : Controller
    {
        //private readonly LendingApplicationService _applicationService;

        private readonly LendingApplicationServiceForEventStore _applicationService;
        private static readonly ILogger Log = Serilog.Log.ForContext<LendingCommandsApi>();

        public LendingCommandsApi(LendingApplicationServiceForEventStore lendingApplicationService)
        {
            _applicationService = lendingApplicationService;
        }

        [HttpPost]
        public Task<IActionResult> Post(Contracts.V1.LendBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);


        [Route("bookId")]
        [HttpPut]
        public Task<IActionResult> Put(Contracts.V1.UpdateBookId request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("userId")]
        [HttpPut]
        public Task<IActionResult> Put(Contracts.V1.UpdateUserId request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);
    }
}
