using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/campaign")]
    public class CampaignController : BaseApiController
    {
        /*
        private readonly ICampaignService _campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        [HttpPost]
        public ActionResult<CampaignResponseDto> SaveCampaign(CampaignCreateDto createDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                createDto.TouristId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _campaignService.CreateCampaign(createDto);
            if(result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }
        [HttpGet]
        [Route("tourist-campaigns")]
        public ActionResult<List<CampaignResponseDto>> GetTouristCampaigns()
        {
            long touristId = -1;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                touristId = long.Parse(identity.FindFirst("id").Value);
            }
            return CreateResponse(_campaignService.GetTouristCampaigns(touristId));
        }
        [HttpGet]
        public ActionResult<TourCampaignResponseDto> GetCampaign(long campaignId)
        {
            var result = _campaignService.GetById(campaignId);
            if(result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }
        */
    }
}
