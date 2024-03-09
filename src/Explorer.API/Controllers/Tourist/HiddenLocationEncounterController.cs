using System.Text.Json;
using System.Text;
using Explorer.Encounters.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/hidden-location-encounter")]
    public class HiddenLocationEncounterController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        public HiddenLocationEncounterController()
        {
        }
        /*
        [HttpGet("{id:long}")]
        public ActionResult<EncounterResponseDto> GetHiddenLocationEncounterById(long id)
        {
            var result = _encounterService.GetHiddenLocationEncounterById(id);
            return CreateResponse(result);
        }
        */
        [HttpPost("{id:long}/complete")]
        public async Task<ActionResult<EncounterResponseDto>> Complete([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            using StringContent jsonContent = new(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/complete/" + id, jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }
        /*

        [HttpPost("{id:long}/check-range")]
        public bool CheckIfUserInCompletionRange([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            return _encounterService.CheckIfUserInCompletionRange(userId, id, position.Longitude, position.Latitude);
        }

        [HttpPost("create")]
        public ActionResult<HiddenLocationEncounterResponseDto> Create([FromBody] HiddenLocationEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var progress = _touristProgressService.GetByUserId(userId);
            if (progress.Value.Level >= 10)
            {
                var result = _encounterService.CreateHiddenLocationEncounter(encounter);
                return CreateResponse(result);

            }
            return CreateResponse(Result.Fail("Tourist level is not high enough."));
        }
        */
    }
}
