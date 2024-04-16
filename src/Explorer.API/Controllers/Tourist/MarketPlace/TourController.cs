using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class TourController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        static readonly HttpClient client = new HttpClient();
        public TourController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/published")]
        public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetPublishedTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetPublishedLimitedView(page, pageSize);
            //return CreateResponse(result);
            return null;
        }

        [HttpGet("tours/{tourId:long}")]
        /*public ActionResult<PagedResult<TourResponseDto>> GetById(long tourId)
        {
            var result = _tourService.GetById(tourId);
            return CreateResponse(result);
        }*/
        public async Task<ActionResult<PagedResult<TourResponseDto>>> GetById(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);

            // Pravljenje URL-a za pozivanje GetByAuthorId metode
            string url = $"http://psw-project-tours-1:88/tours/getTour/{tourId}";

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

        [HttpGet("tours/can-be-rated/{tourId:long}")]
        public bool CanTourBeRated(long tourId)
        {
            //long userId = extractUserIdFromHttpContext();
            //return _tourService.CanTourBeRated(tourId, userId).Value;
            return true;
        }

        private long extractUserIdFromHttpContext()
        {
            return long.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("id")?.Value);
        }

        [Authorize(Policy = "touristPolicy")]
        [Authorize(Roles = "tourist")]
        [HttpGet("tours/inCart/{id:long}")]
        public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetToursInCart([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            //var cart = _shoppingCartService.GetByTouristId(id);
            //if (cart.Value == null)
            //{
            //    return NotFound();
            //}
            //var tourIds = cart.Value.OrderItems.Select(order => order.TourId).ToList();
            //var result = _tourService.GetLimitedInfoTours(page, pageSize, tourIds);
            //return CreateResponse(result);
            return null;
        }
        //[HttpGet("tours/inCart/{id:long}")]
        //public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetToursInCart([FromQuery] int page, [FromQuery] int pageSize, long id)
        //{
        //    //var cart = _shoppingCartService.GetByTouristId(id);
        //    //if (cart == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //var tourIds = cart.Value.OrderItems.Select(order => order.TourId).ToList();
        //    //var result = _tourService.GetLimitedInfoTours(page, pageSize, tourIds);
        //    //return CreateResponse(result);
        //    return null;
        //}

        [HttpGet("tours/adventure")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularAdventureTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetAdventureTours(page, pageSize);
            //return CreateResponse(result);
            return null;
        }

        [HttpGet("tours/family")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularFamilyTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetFamilyTours(page, pageSize);
            //return CreateResponse(result);
            return null;
        }

        [HttpGet("tours/cruise")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularCruiseTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetCruiseTours(page, pageSize);
            //return CreateResponse(result);
            return null;
        }

        [HttpGet("tours/cultural")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularCulturalTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetCulturalTours(page, pageSize);
            //return CreateResponse(result);
            return null;
        }
        
    }
}
