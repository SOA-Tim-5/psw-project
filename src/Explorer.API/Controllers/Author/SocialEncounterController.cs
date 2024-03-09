using System.Text;
using System.Text.Json;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/social-encounter")]
    public class SocialEncounterController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        public SocialEncounterController()
        {
        }

        [HttpPost("create")]
        public async Task<ActionResult<EncounterResponseDto>> Create([FromBody] SocialEncounterCreateDto encounter)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(encounter), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/social", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }
    }
}
