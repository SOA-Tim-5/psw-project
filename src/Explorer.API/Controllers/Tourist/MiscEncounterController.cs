using Explorer.Encounters.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Explorer.API.EncountersDtos;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/misc-encounter")]
    public class MiscEncounterController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        public MiscEncounterController() { }


        [HttpPost("createMisc")]
        public async Task<ActionResult<MiscEncounterResponseDto>> Create([FromBody] MiscEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            string url = $"http://localhost:81/encounters/touristProgress/{userId}?";

            using HttpResponseMessage touristProgressResponse = await client.GetAsync(url);
            if(touristProgressResponse.IsSuccessStatusCode)
            {
                var touristProgress = await touristProgressResponse.Content.ReadAsStringAsync();
                var touristProgressModel = JsonSerializer.Deserialize<TouristProgressDto>(touristProgress);
                if (touristProgressModel.Level>=10)
                {
                    using StringContent jsonContent = new(JsonSerializer.Serialize(encounter), Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/misc", jsonContent);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return CreateResponse(jsonResponse.ToResult());
                }

            }
           return CreateResponse(Result.Fail("Tourist level is not high enough.").ToResult());
            
        }
        
    }
}
