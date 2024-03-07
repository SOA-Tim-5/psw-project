using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourrecommenders")]
    public class TourRecommendersController : BaseApiController
    {
        /*
        private readonly IToursRecommendersService _tourRecommendersService;
        public TourRecommendersController(IToursRecommendersService tourRecommendersService)
        {
            _tourRecommendersService = tourRecommendersService;
        }
        [Route("activetours")]
        [HttpGet]
        public ActionResult<PagedResult<TourResponseDto>> getActiveTours()
        {
            long id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourRecommendersService.GetActiveTours(id);
            return CreateResponse(result);
        }
        [Route("recommendedtours")]
        [HttpGet]
        public ActionResult<PagedResult<TourResponseDto>> getRecommendedTours()
        {
            long id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourRecommendersService.GetRecommendedTours(id);
            return CreateResponse(result);
        }
        */
    }
}
