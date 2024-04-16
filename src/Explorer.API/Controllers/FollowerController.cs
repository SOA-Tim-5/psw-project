using Explorer.API.FollowerDtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/follower")]
    public class FollowerController : BaseApiController
    {

        static readonly HttpClient client = new HttpClient();
        private readonly IFollowerService _followerService;
        private readonly IUserService _userService;
        public FollowerController(IFollowerService followerService, IUserService userService)
        {
            _followerService = followerService;
            _userService = userService;
        }

        [HttpGet("followers/{id:long}")]
        public ActionResult<PagedResult<FollowerResponseWithUserDto>> GetFollowers([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            long userId = id;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _followerService.GetFollowers(page, pageSize, userId);
            return CreateResponse(result);
        }
        [HttpGet("followings/{id:long}")]
        public ActionResult<PagedResult<FollowingResponseWithUserDto>> GetFollowings([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            long userId = id;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _followerService.GetFollowings(page, pageSize, userId);
            return CreateResponse(result);
        }
        [HttpGet("user-followings/{id}")]
        public async Task<ActionResult<List<FollowingResponseDto>>> GetUserFollowings(string id)
        {
            var followings = await client.GetFromJsonAsync<FollowingResponseDto[]>(
                "http://localhost:8090/user-followings/" + id);
            return followings.ToList();
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _followerService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<FollowerResponseDto> Create([FromBody] FollowerCreateDto follower)
        {
            var result = _followerService.Create(follower);
            return CreateResponse(result);
        }

        [HttpGet("search/{searchUsername}")]
        public ActionResult<PagedResult<UserResponseDto>> GetSearch([FromQuery] int page, [FromQuery] int pageSize, string searchUsername)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _userService.SearchUsers(0, 0, searchUsername, userId);
            return CreateResponse(result);
        }

        [HttpPost("create-following")]
        public async Task<ActionResult<FollowerResponseDto>> CreateNewFollowing([FromBody] UserFollowingDto following)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(following), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:8090/follower/create", jsonContent);
            var res = await response.Content.ReadFromJsonAsync<FollowerResponseDto>();
            Console.WriteLine("JSONCONTENT "+ jsonContent);

            
            return res;
        }
    }


}
