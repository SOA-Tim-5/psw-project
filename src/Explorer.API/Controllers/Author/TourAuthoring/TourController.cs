using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Author.TourAuthoring
{

    [Route("api/tour")] 
    public class TourController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        public TourController()
        {
        }

        [Authorize(Roles = "author")]
        [HttpGet]
        public ActionResult<PagedResult<TourResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetAllPaged(page, pageSize);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author")]
        [HttpGet("published")]
        public ActionResult<PagedResult<TourResponseDto>> GetPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourService.GetPublished(page, pageSize);
            //return CreateResponse(result);
            return null;
        }



        //**************************************************
        [Authorize(Roles = "author, tourist")]
        [HttpGet("authors")]
        public async Task<ActionResult<List<TourResponseDto>>> GetAuthorsTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);

            // Pravljenje URL-a za pozivanje GetByAuthorId metode
            string url = $"http://psw-project-tours-1:88/tours/get/{id}?page={page}&pageSize={pageSize}";

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


        /*    [Authorize(Roles = "author, tourist")]
            [HttpGet("authors")]
            public ActionResult<PagedResult<TourResponseDto>> GetAuthorsTours([FromQuery] int page, [FromQuery] int pageSize)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var id = long.Parse(identity.FindFirst("id").Value);
                var result = _tourService.GetAuthorsPagedTours(id, page, pageSize);
                return CreateResponse(result);
            }*/

         /*   [Authorize(Roles = "author, tourist")]
            [HttpPost]
            public ActionResult<TourResponseDto> Create([FromBody] TourCreateDto tour)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
                }
                var result = _tourService.Create(tour);
                return CreateResponse(result);
            }

        */
            //KREIRANJE TURE 1. FUNKCIONALNOST
        [Authorize(Roles = "author, tourist")]
        [HttpPost]
        public async Task<ActionResult<TourResponseDto>> Create([FromBody] TourCreateDto tour)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }
            using StringContent jsonContent = new(JsonSerializer.Serialize(tour), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://psw-project-tours-1:88/tour/create", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPut("{id:int}")]
        public ActionResult<TourResponseDto> Update([FromBody] TourUpdateDto tour)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //if (identity != null && identity.IsAuthenticated)
            //{
            //    tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
            //}
            //var result = _tourService.Update(tour);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            //var result = _tourService.DeleteCascade(id);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("equipment/{tourId:int}")]
        public ActionResult GetEquipment(int tourId)
        {
            //var result = _tourService.GetEquipment(tourId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPost("equipment/{tourId:int}/{equipmentId:int}")]
        public ActionResult AddEquipment(int tourId, int equipmentId)
        {
            //var result = _tourService.AddEquipment(tourId, equipmentId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpDelete("equipment/{tourId:int}/{equipmentId:int}")]
        public ActionResult DeleteEquipment(int tourId, int equipmentId)
        {
            //var result = _tourService.DeleteEquipment(tourId, equipmentId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("{tourId:long}")]
        public ActionResult<PagedResult<TourResponseDto>> GetById(long tourId)
        {
            //var result = _tourService.GetById(tourId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author")]
        [HttpPut("publish/{id:int}")]
        public ActionResult<TourResponseDto> Publish(long id)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //long authorId = -1;
            //if (identity != null && identity.IsAuthenticated)
            //{
            //    authorId = long.Parse(identity.FindFirst("id").Value);
            //}
            //var result = _tourService.Publish(id, authorId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "author")]
        [HttpPut("archive/{id:int}")]
        public ActionResult<TourResponseDto> Archive(long id)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //long authorId = -1;
            //if (identity != null && identity.IsAuthenticated)
            //{
            //    authorId = long.Parse(identity.FindFirst("id").Value);
            //}
            //var result = _tourService.Archive(id, authorId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "tourist")]
        [HttpPut("markAsReady/{id:int}")]
        public ActionResult<TourResponseDto> MarkAsReady(long id)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //long authorId = -1;
            //if (identity != null && identity.IsAuthenticated)
            //{
            //    authorId = long.Parse(identity.FindFirst("id").Value);
            //}
            //var result = _tourService.MarkAsReady(id, authorId);
            //return CreateResponse(result);
            return null;
        }

        [Authorize(Roles = "tourist")]
        [HttpGet("recommended/{publicKeyPointIds}")]
        public ActionResult<TourResponseDto> GetRecommended([FromQuery] int page, [FromQuery] int pageSize, string publicKeyPointIds)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //long authorId = -1;
            //if (identity != null && identity.IsAuthenticated)
            //{
            //    authorId = long.Parse(identity.FindFirst("id").Value);
            //}

            //var keyValuePairs = publicKeyPointIds.Split('=');

            //var keyPointIdsList = keyValuePairs[1].Split(',').Select(long.Parse).ToList();

            //var result = _tourService.GetToursBasedOnSelectedKeyPoints(page, pageSize, keyPointIdsList, authorId);
            //return CreateResponse(result);
            return null;
        }
        
    }
}
