using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post(V1.Create request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("title")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetTitle request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("description")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateDescription request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("requestToPublish")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.RequestToPublish request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.Publish request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

    }
}
