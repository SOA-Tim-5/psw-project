﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/facility")]
    public class FacilityController : BaseApiController
    {
        private readonly IFacilityService _facilityService;
        static readonly HttpClient client = new HttpClient();
        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet]
        public ActionResult<PagedResult<FacilityResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _facilityService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("authorsFacilities")]
        public ActionResult<PagedResult<FacilityResponseDto>> GetByAuthorId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInAuthorId = int.Parse(identity.FindFirst("id").Value);
            var result = _facilityService.GetPagedByAuthorId(page, pageSize, loggedInAuthorId);
            return CreateResponse(result);
        }

        /*[HttpPost]
        public ActionResult<FacilityResponseDto> Create([FromBody] FacilityCreateDto facility)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                facility.AuthorId = int.Parse(identity.FindFirst("id").Value);
            }

            var result = _facilityService.Create(facility);

            return CreateResponse(result);
        }*/

        
        [HttpPost]
        public async Task<ActionResult<FacilityResponseDto>> Create([FromBody] FacilityCreateDto facility)
        {
            //var result = _keyPointService.Create(keyPoint);
            using StringContent jsonContent = new(JsonSerializer.Serialize(facility), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:88/facility/create", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
            //return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<FacilityResponseDto> Update([FromBody] FacilityUpdateDto facility)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                facility.AuthorId = int.Parse(identity.FindFirst("id").Value);
            }

            var result = _facilityService.Update(facility);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _facilityService.Delete(id);
            return CreateResponse(result);
        }
    }
}
