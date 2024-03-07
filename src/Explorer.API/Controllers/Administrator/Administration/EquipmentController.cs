using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;
        static readonly HttpClient client = new HttpClient();

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EquipmentResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentResponseDto>> Create([FromBody] EquipmentCreateDto equipment)
        {
            //var result = _equipmentService.Create(equipment);
            //return CreateResponse(result);
            using StringContent jsonContent = new(JsonSerializer.Serialize(equipment), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("http://localhost:88/equipment/create", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpPut("{id:long}")]
        public ActionResult<EquipmentResponseDto> Update([FromBody] EquipmentUpdateDto equipment)
        {
            var result = _equipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
