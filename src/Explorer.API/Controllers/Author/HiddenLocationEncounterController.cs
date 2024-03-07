using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/hidden-location-encounter")]
    public class HiddenLocationEncounterController : BaseApiController
    {
    /*
        private readonly IEncounterService _encounterService;
        public HiddenLocationEncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPost("create")]
        public ActionResult<HiddenLocationEncounterResponseDto> Create([FromBody] HiddenLocationEncounterCreateDto encounter)
        {
            var result = _encounterService.CreateHiddenLocationEncounter(encounter);
            return CreateResponse(result);
        }
    */
}
}
