using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "nonAdministratorPolicy")]
[Route("api/people")]
public class PersonController : BaseApiController
{
    //private readonly IPersonService _personService;
    static readonly HttpClient client = new HttpClient();
    public PersonController()
    {
        //_personService = personService;
    }

//    [HttpPut("update/{personId:long}")]
//    public ActionResult<PersonResponseDto> Update([FromBody] PersonUpdateDto person, long personId)
//    {
//        var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
//        var response = _personService.Get(personId);
//        if (response.IsFailed) return CreateResponse(response);

//        var userId = response.Value.UserId;
//        if (loggedInUserId == userId)
//        {
//            person.Id = personId;
//            var result = _personService.UpdatePerson(person);
//            return CreateResponse(result);
//        }
//        return Forbid();
//    }

//    [HttpGet]
//    public ActionResult<PersonResponseDto> GetPaged(int page, int pageSize)
//    {
//        var result = _personService.GetAll(page, pageSize);
//        return CreateResponse(result);
//    }

    [HttpGet("person/{userId:long}")]
    public async Task<ActionResult<PersonResponseDto>> GetByUserId(long userId)
    {
        string url = $"https://localhost:44332/api/people/person/" + userId;

        // Slanje GET zahteva
        using HttpResponseMessage response = await client.GetAsync(url);

        // Provera status koda odgovora
        if (response.IsSuccessStatusCode)
        {
            // Čitanje odgovora kao string
            string jsonResponse = await response.Content.ReadAsStringAsync();
            //var resultModel = JsonSerializer.Deserialize<List<TourResponseDto>>(jsonResponse);

            // Kreiranje odgovora
            return CreateResponse(jsonResponse.ToResult());
        }
        else
        {
            return StatusCode((int)response.StatusCode);
        }
    }

}