using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class KeyPointController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();
        public KeyPointController()
        {
        }

        [Authorize(Roles = "author, tourist")]
        //[HttpGet("tours/{tourId:long}/key-points")]
        /*public ActionResult<KeyPointResponseDto> GetKeyPoints(long tourId)
        {
            var result = _keyPointService.GetByTourId(tourId);
            return CreateResponse(result);
        }*/

        public async Task<ActionResult<KeyPointResponseDto>> GetKeyPoints(long tourId)
        {
            
            // Pravljenje URL-a za pozivanje GetByAuthorId metode
            string url = $"http://host.docker.internal:88/tours/getKeypoints/{tourId}";

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

        [Authorize(Roles = "tourist")]
        [HttpGet("{campaignId:long}/key-points")]
        public ActionResult<KeyPointResponseDto> GetCampaignKeyPoints(long campaignId)
        {
            //var result = _keyPointService.GetByCampaignId(campaignId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/{tourId:long}/firts-key-point")]
        public ActionResult<KeyPointResponseDto> GetToursFirstKeyPoint(long tourId)
        {
            //var result = _keyPointService.GetFirstByTourId(tourId);
            //return CreateResponse(result);
            return null;
        }
    }
}
