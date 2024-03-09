using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Author
{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/publicFacilityRequest")]
    public class PublicFacilityRequestController : BaseApiController
    {
        
        private readonly IUserService _userService;
        static readonly HttpClient client = new HttpClient();

        public PublicFacilityRequestController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<PublicFacilityRequestResponseDto>> Create([FromBody] PublicFacilityRequestCreateDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                request.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }
            //_userService.Get(long.Parse(identity.FindFirst("id").Value));
            request.Created = DateTime.UtcNow;
            request.AuthorName = _userService.Get(long.Parse(identity.FindFirst("id").Value)).Value.Username;
            request.AuthorPicture = _userService.Get(long.Parse(identity.FindFirst("id").Value)).Value.ProfilePicture;
            using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:88/publicFacilityRequest/create", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }
        
    }
}
