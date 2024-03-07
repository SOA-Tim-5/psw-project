using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/publicKeyPointRequest")]
    public class PublicKeyPointRequestController:BaseApiController
    {
        /*
        private readonly IPublicKeyPointRequestService _requestService;

        public PublicKeyPointRequestController(IPublicKeyPointRequestService requestService)
        {
            _requestService = requestService;
        }
        [HttpPost]
        public ActionResult<PublicKeyPointRequestResponseDto> Create([FromBody] PublicKeyPointRequestCreateDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                request.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }
            request.Created = DateTime.UtcNow;
            var result = _requestService.Create(request);
            return CreateResponse(result);
        }
        */
    }
}
