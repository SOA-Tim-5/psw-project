using System.Text;
using System.Text.Json;
using Explorer.API.EncountersDtos;
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
       
        [HttpGet("{id:long}")]
        public async Task<ActionResult<EncounterResponseDto>> GetHiddenLocationEncounterById(long id)
        {
            string url = "http://localhost:81/encounters/hidden/"+id.ToString();

            using HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var resultModel = JsonSerializer.Deserialize<HiddenLocationEncounterResponseDto>(result);
            return CreateResponse(resultModel.ToResult());
        }

        [HttpPost("{id:long}/complete")]
        public async Task<ActionResult<EncounterResponseDto>> Complete([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            using StringContent jsonContent = new(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/complete/" + id, jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }
        
        [HttpPost("{id:long}/check-range")]
        public async Task<bool>  CheckIfUserInCompletionRange([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            string url = "http://localhost:81/encounters/isInRange/" + id.ToString() + "/" + position.Longitude.ToString() + "/" + position.Latitude.ToString();

            using HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var resultModel = JsonSerializer.Deserialize<bool>(result);

            return resultModel;
            
        }

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
