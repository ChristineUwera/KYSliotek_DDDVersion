using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    [Route("/user")]
    public class UserProfileCommandsApi : Controller
    {
        private readonly UserProfileApplicationService _applicationService;
        private static readonly ILogger Log = Serilog.Log.ForContext<UserProfileCommandsApi>();
        //private readonly UserProfileApplicationServiceForEventStore _applicationService;

        public UserProfileCommandsApi(UserProfileApplicationService appliactionService)                     //UserProfileApplicationServiceForEventStore
        {
            _applicationService = appliactionService;
        }

        [HttpPost]
        public Task<IActionResult> Post(Contracts.V1.RegisterUser request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("fullname")]
        [HttpPut]
        public Task<IActionResult> Put(Contracts.V1.UpdateUserFullName request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);


        [Route("email")]
        [HttpPut]
        public Task<IActionResult> Put(Contracts.V1.UpdateUserEmail request)
              => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);
    }//for email validation, we should send an email verification to the registered user.
    //for them to confirm the email. 
}
