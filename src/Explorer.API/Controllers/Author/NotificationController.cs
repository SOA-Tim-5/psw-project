using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/notifications")]
    public class NotificationController : BaseApiController
    {
        /*
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public ActionResult<PublicFacilityNotificationResponseDto> GetFacilityNotificationsByAuthorId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id= long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _notificationService.GetFacilityNotificationsByAuthorId(page,pageSize,id);
            return CreateResponse(result);
        }
        [HttpGet("keypoint")]
        public ActionResult<PublicKeyPointNotificationResponseDto> GetKeyPointNotificationsByAuthorId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _notificationService.GetKeyPointNotificationsByAuthorId(page, pageSize, id);
            return CreateResponse(result);
        }
        */
    }
}
