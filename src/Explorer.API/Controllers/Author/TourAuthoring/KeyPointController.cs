using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Author.TourAuthoring;

[Route("api/tour-authoring")]
public class KeyPointController : BaseApiController
{
    
    static readonly HttpClient client = new HttpClient();

    public KeyPointController()
    {
    }

    //****************************************
    [Authorize(Roles = "author")]
    [HttpPost("tours/{tourId:long}/key-points")]
    public async Task<ActionResult<KeyPointResponseDto>> CreateKeyPoint([FromRoute] long tourId, [FromBody] KeyPointCreateDto keyPoint)
    {
            
        keyPoint.TourId = tourId;
        //var result = _keyPointService.Create(keyPoint);
        using StringContent jsonContent = new(JsonSerializer.Serialize(keyPoint), Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PostAsync("http://host.docker.internal:88/keypoint/create", jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return CreateResponse(jsonResponse.ToResult());
        //return CreateResponse(result);
    }

    [Authorize(Roles = "author")]
    [HttpPut("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult<KeyPointResponseDto> Update(long tourId, long id, [FromBody] KeyPointUpdateDto keyPoint)
    {
        //keyPoint.Id = id;
        //var result = _keyPointService.Update(keyPoint);
        //return CreateResponse(result);
        return null;
    }

    [Authorize(Roles = "author, tourist")]
    [HttpDelete("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult Delete(long tourId, long id)
    {
        //var result = _keyPointService.Delete(id);
        //return CreateResponse(result);
        return null;
    }

    [Authorize(Roles = "author")]
    [HttpGet]
    public ActionResult<PagedResult<KeyPointResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        //var result = _keyPointService.GetPaged(page, pageSize);
        //return CreateResponse(result);
        return null;
    }
    
}
