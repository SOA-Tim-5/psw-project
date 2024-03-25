using Explorer.API.EncountersDtos;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/social-encounter")]
    public class SocialEncounterController : BaseApiController
    {

        static readonly HttpClient client = new HttpClient();

        public SocialEncounterController()
        {
           
        }

        [HttpPost("create")]
        public async Task<ActionResult<EncounterResponseDto>> Create([FromBody] SocialEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            string url = $"http://host.docker.internal:81/encounters/touristProgress/{userId}?";

            using HttpResponseMessage touristProgressResponse = await client.GetAsync(url);
            if (touristProgressResponse.IsSuccessStatusCode)
            {
                var touristProgress = await touristProgressResponse.Content.ReadAsStringAsync();
                var touristProgressModel = JsonSerializer.Deserialize<TouristProgressDto>(touristProgress);
                if (touristProgressModel.Xp >= 10)
                {
                    using StringContent jsonContent = new(JsonSerializer.Serialize(encounter), Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = await client.PostAsync("http://host.docker.internal:81/encounters/social", jsonContent);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return CreateResponse(jsonResponse.ToResult());
                }

            }
            return CreateResponse(Result.Fail("Tourist level is not high enough.").ToResult());
        }
        
    }
}
