using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using static KYSliotek.Commands.Contracts;

namespace KYSliotek.Books
{
    [Route("/books")]
    public class BooksCommandsApi: Controller
    {
        private readonly BooksApplicationService _applicationService;

        public BooksCommandsApi(BooksApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public Task<IActionResult> Post(V1.Create request)
        {
            return HandleRequest(request, _applicationService.Handle);
        }

        [Route("title")]
        [HttpPut]
        public Task<IActionResult> Put(V1.SetTitle request)
        {
            return HandleRequest(request, _applicationService.Handle);
        }

        [Route("description")]
        [HttpPut]
        public Task<IActionResult> Put(V1.UpdateDescription request)
        {
            return HandleRequest(request, _applicationService.Handle);
        }

        [Route("requestToPublish")]
        [HttpPut]
        public Task<IActionResult> Put(V1.RequestToPublish request)
        {
            //await _applicationService.Handle(request);
            //return Ok();
            return HandleRequest(request, _applicationService.Handle);
        }

        [Route("publish")]
        [HttpPut]
        public Task<IActionResult> Put(V1.Publish request)
        {
            //await _applicationService.Handle(request);
            //return Ok();
            return HandleRequest(request, _applicationService.Handle);
        }

        private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
        {
            try
            {
                Log.Debug("Handling HTTP request of type {type}", typeof(T).Name);
                await handler(request);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error handling the request");
                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}
