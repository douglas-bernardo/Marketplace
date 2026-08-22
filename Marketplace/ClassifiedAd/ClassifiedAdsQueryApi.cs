using Marketplace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net;
using ILogger = Serilog.ILogger;

namespace Marketplace.ClassifiedAd
{
    /// <summary>
    /// API controller for handling queries related to classified ads. It provides endpoints to retrieve published classified ads, the owner's classified ads, and public classified ad details.
    /// </summary>
    /// <param name="connection"></param>
    [Route("/ad")]
    public class ClassifiedAdsQueryApi(DbConnection connection) : Controller
    {
        private static ILogger _log = Serilog.Log.ForContext<ClassifiedAdsQueryApi>();
        private readonly DbConnection _connection = connection;

        [HttpGet]
        [Route("list")]
        public Task<IActionResult> Get(QueryModels.GetPublishedClassifiedAds request)
            => RequestHandler.HandleQuery(() => _connection.Query(request), _log);

        [HttpGet]
        [Route("myads")]
        public Task<IActionResult> Get(QueryModels.GetOwnersClassifiedAd request)
            => RequestHandler.HandleQuery(() => _connection.Query(request), _log);

        [HttpGet]
        public Task<IActionResult> Get(QueryModels.GetPublicClassifiedAd request)
            => RequestHandler.HandleQuery(() => _connection.Query(request), _log);
    }
}
