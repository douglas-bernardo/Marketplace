using Marketplace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Marketplace.ClassifiedAd
{
    /// <summary>
    /// API controller for handling commands related to classified ads. It provides endpoints to create, update, and publish classified ads.
    /// </summary>
    [Route("/ad")]
    public class ClassifiedAdsCommandsApi : Controller
    {
        private readonly ClassifiedAdsApplicationService _applicationService;
        private static readonly ILogger Log = Serilog.Log.ForContext<ClassifiedAdsCommandsApi>();

        public ClassifiedAdsCommandsApi(
            ClassifiedAdsApplicationService applicationService)
            => _applicationService = applicationService;

        [HttpPost]
        public Task<IActionResult> Post(Commands.V1.Create request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("name")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.SetTitle request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("text")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.UpdateText request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("price")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.UpdatePrice request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("requestpublish")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.RequestToPublish request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);

        [Route("publish")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.Publish request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, Log);
    }
}