using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/requests")]
    public class RequestManagingController : BaseApiController
    {
        private readonly IPublicKeyPointRequestService _publicKeyPointRequestService;
        private readonly IPublicFacilityRequestService _publicFacilityRequestService;
        static readonly HttpClient client = new HttpClient();
        public RequestManagingController(IPublicKeyPointRequestService publicKeyPointRequestService, IPublicFacilityRequestService publicFacilityRequestService)
        {
            _publicKeyPointRequestService = publicKeyPointRequestService;
            _publicFacilityRequestService = publicFacilityRequestService;
        }
        [HttpGet]
        public async  Task<ActionResult<PagedResult<PublicKeyPointRequestResponseDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            /*var result = _publicKeyPointRequestService.GetPagedWithName(page, pageSize);
            return CreateResponse(result);*/
            string url = $"http://localhost:88/publicKeyPointRequest/get/?page={page}&pageSize={pageSize}";

            // Slanje GET zahteva
            using HttpResponseMessage response = await client.GetAsync(url);

            // Provera status koda odgovora
            if (response.IsSuccessStatusCode)
            {
                // Čitanje odgovora kao string
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Kreiranje odgovora
                return CreateResponse(jsonResponse.ToResult());
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpPut("{id:long}")]
        public ActionResult<PublicKeyPointRequestResponseDto> Update([FromBody] PublicKeyPointRequestUpdateDto response)
        {
            var result = _publicKeyPointRequestService.Update(response);
            return CreateResponse(result);
        }
        [HttpGet("facility")]
        public ActionResult<PagedResult<PublicFacilityRequestResponseDto>> GetAllFacilityRequest([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicFacilityRequestService.GetPagedWithName(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPut("facility/{id:long}")]
        public ActionResult<PublicFacilityRequestResponseDto> UpdateFacility([FromBody] PublicFacilityRequestUpdateDto response)
        {
            var result = _publicFacilityRequestService.Update(response);
            return CreateResponse(result);
        }
        [HttpPatch("reject/{id:long}/{comment}")]
        public ActionResult RejectKeyPointRequest(long id, string comment)
        {
            var loggedUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _publicKeyPointRequestService.Reject(id, comment, loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("accept/{id:long}")]
        public ActionResult AcceptKeyPointRequest(long id)
        {
            var loggedUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _publicKeyPointRequestService.Accept(id, loggedUserId);
            return CreateResponse(result);
        }
        [HttpPatch("facility/reject/{id:long}/{comment}")]
        public ActionResult RejectFacilityRequest(long id, string comment)
        {
            var loggedUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _publicFacilityRequestService.Reject(id, comment, loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("facility/accept/{id:long}")]
        public ActionResult AcceptFacilityRequest(long id)
        {
            var loggedUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _publicFacilityRequestService.Accept(id, loggedUserId);
            return CreateResponse(result);
        }
    }
}
