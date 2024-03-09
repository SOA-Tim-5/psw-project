using Explorer.API.EncountersDtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Explorer.Encounters.API.Dtos;

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

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterResponseDto> Complete([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteHiddenLocationEncounter(userId, id, position.Longitude, position.Latitude);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/check-range")]
        public bool CheckIfUserInCompletionRange([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            return _encounterService.CheckIfUserInCompletionRange(userId, id, position.Longitude, position.Latitude);
        }
        */
        [HttpPost("create")]
        public async Task<ActionResult<HiddenLocationEncounterResponseDto>> Create([FromBody] HiddenLocationEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            string url = $"http://localhost:81/encounters/touristProgress/{userId}?";

            using HttpResponseMessage touristProgressResponse = await client.GetAsync(url);
            if (touristProgressResponse.IsSuccessStatusCode)
            {
                var touristProgress = await touristProgressResponse.Content.ReadAsStringAsync();
                var touristProgressModel = JsonSerializer.Deserialize<TouristProgressDto>(touristProgress);
                if (touristProgressModel.Xp >= 10)
                {
                    using StringContent jsonContent = new(JsonSerializer.Serialize(encounter), Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/hidden", jsonContent);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return CreateResponse(jsonResponse.ToResult());
                }

            }
            return CreateResponse(Result.Fail("Tourist level is not high enough.").ToResult());
        }
        
    }
}
