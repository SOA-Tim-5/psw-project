//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;
//using Explorer.BuildingBlocks.Core.UseCases;
//using Explorer.Tours.API.Dtos;
//using Explorer.Tours.Core.UseCases;
//using FluentResults;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Explorer.API.Controllers.Author
//{
//    [Authorize(Policy = "authorPolicy")]
//    [Route("api/publicKeyPointRequest")]
//    public class PublicKeyPointRequestController:BaseApiController
//    {
        
//        private readonly IUserService _userService;
//        static readonly HttpClient client = new HttpClient();

//        public PublicKeyPointRequestController(IUserService userService)
//        {
//            _userService = userService;
//        }
//        [HttpPost]
//        public async  Task<ActionResult<PublicKeyPointRequestResponseDto>> Create([FromBody] PublicKeyPointRequestCreateDto request)
//        {
//            var identity = HttpContext.User.Identity as ClaimsIdentity;
//            if (identity != null && identity.IsAuthenticated)
//            {
//                request.AuthorId = long.Parse(identity.FindFirst("id").Value);
//            }
//            //_userService.Get(long.Parse(identity.FindFirst("id").Value));
//            request.Created = DateTime.UtcNow;
//            request.AuthorName = _userService.Get(long.Parse(identity.FindFirst("id").Value)).Value.Username;
//            request.AuthorPicture = _userService.Get(long.Parse(identity.FindFirst("id").Value)).Value.ProfilePicture;
//            using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
//            using HttpResponseMessage response = await client.PostAsync("http://host.docker.internal:88/publicKeyPointRequest/create", jsonContent);
//            var jsonResponse = await response.Content.ReadAsStringAsync();
//            return CreateResponse(jsonResponse.ToResult());
//        }
        
//    }
//}
