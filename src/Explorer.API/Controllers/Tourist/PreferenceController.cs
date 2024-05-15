using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/preferences")]
    public class PreferenceController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();
        public PreferenceController()
        {
        }

        //[HttpGet]
        /* public ActionResult<PreferenceResponseDto> Get()
         {
            // int userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
             int id = 0;
             var identity = HttpContext.User.Identity as ClaimsIdentity;
             if (identity != null && identity.IsAuthenticated)
             {
                 id = int.Parse(identity.FindFirst("id").Value);
             }
             var result = _tourPreferencesService.GetByUserId(id);
             return CreateResponse(result);
         }*/
        public async  Task<ActionResult<PreferenceResponseDto>> GetByTouristId()
        { int id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = int.Parse(identity.FindFirst("id").Value);
            }
            string url = $"http://host.docker.internal:88/preferences/get/{id}";

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

        //[HttpPost("create")]
        /*public ActionResult<PreferenceResponseDto> Create([FromBody] PreferenceCreateDto preference)
        {
            preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _tourPreferencesService.Create(preference);
            return CreateResponse(result);
        }*/
       public async  Task<ActionResult<PreferenceResponseDto>> Create([FromBody] PreferenceCreateDto preference)
       {
           preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
           using StringContent jsonContent = new(JsonSerializer.Serialize(preference), Encoding.UTF8, "application/json");
           using HttpResponseMessage response = await client.PostAsync("http://host.docker.internal:88/preference/create", jsonContent);
           var jsonResponse = await response.Content.ReadAsStringAsync();
           return CreateResponse(jsonResponse.ToResult());
       }

        //[HttpDelete("{id:int}")]
        //public ActionResult Delete(int id)
        //{
        //    var result = _tourPreferencesService.Delete(id);
        //    return CreateResponse(result);
        //}

        //[HttpPut]
        //public ActionResult<PreferenceResponseDto> Update([FromBody] PreferenceUpdateDto preference)
        //{
        //    preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
        //    var result = _tourPreferencesService.Update(preference);
        //    return CreateResponse(result);
        //}
        
    }
}
