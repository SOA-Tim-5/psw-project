using System.Text.Json;
using Explorer.API.EncountersDtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter")]
    public class EncounterController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        public EncounterController()
        {
        }
        
        [HttpGet("{encounterId:long}/instance")]
        public async Task<ActionResult<EncounterResponseDto>> GetInstance(long encounterId)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            string url = "http://localhost:81/encounters/instance/" + userId.ToString() + "/" + encounterId.ToString() + "/encounter";
            using HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var resultModel = JsonSerializer.Deserialize<EncounterInstanceResponseDto>(result);


            return CreateResponse(resultModel.ToResult());
        }
        /*
        [HttpPost("{id:long}/activate")]
        public async Task<ActionResult<EncounterResponseDto>> Activate([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            using StringContent jsonContent = new(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:81/encounters/activate/" + id, jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        /*
        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterResponseDto> Complete(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteEncounter(userId, id);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}/cancel")]
        public ActionResult<EncounterResponseDto> Cancel(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CancelEncounter(userId, id);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<EncounterResponseDto> Get(long id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }
        */
        [HttpGet]
        public async Task<ActionResult<PagedResult<EncounterResponseDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            string url = "http://localhost:81/encounters";

            using HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return CreateResponse(jsonResponse.ToResult());
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpPost("in-range-of")]
        public async Task<ActionResult<PagedResult<EncounterResponseDto>>> GetAllInRangeOf([FromBody] UserPositionWithRangeDto position, [FromQuery] int page, [FromQuery] int pageSize)
        {
            string url = "http://localhost:81/encounters/"+position.Range.ToString()+"/"+position.Longitude.ToString()+"/"+position.Latitude.ToString();

            using HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return CreateResponse(jsonResponse.ToResult());
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }


        [HttpGet("done-encounters")]
        public async Task<ActionResult<PagedResult<EncounterResponseDto>>> GetAllDoneByUser(int currentUserId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            string url = $"http://localhost:81/encounters/doneByUser/{currentUserId}?";

            using HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return CreateResponse(jsonResponse.ToResult());
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        /*

        [HttpGet("active")]
        public ActionResult<PagedResult<EncounterResponseDto>> GetActive([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetActive(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPost("key-point/{keyPointId:long}")]
        public ActionResult<KeyPointEncounterResponseDto> ActivateKeyPointEncounter(
            [FromBody] TouristPositionCreateDto position, long keyPointId)
        {
            long userId = int.Parse(HttpContext.User.Claims
                .First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result =
                _encounterService.ActivateKeyPointEncounter(position.Longitude, position.Latitude, keyPointId, userId);

            return CreateResponse(result);
        }
        */
        [HttpGet("progress")]
        public async Task<ActionResult<TouristProgressDto>> GetProgress()
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            string url = $"http://localhost:81/encounters/touristProgress/{userId}?";

            using HttpResponseMessage touristProgressResponse = await client.GetAsync(url);
           
            var touristProgress = await touristProgressResponse.Content.ReadAsStringAsync();
            var touristProgressModel = JsonSerializer.Deserialize<TouristProgressDto>(touristProgress);


            return CreateResponse(touristProgressModel.ToResult());
            
        }
        
    }
}
