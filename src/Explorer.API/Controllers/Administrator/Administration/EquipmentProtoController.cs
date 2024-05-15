using System.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Administrator.Administration
{
    public class EquipmentProtoController : EquipmentService.EquipmentServiceBase
    {
        private readonly ILogger<EquipmentProtoController> _logger;

        public EquipmentProtoController(ILogger<EquipmentProtoController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "administrator")]
        public override async Task<EquipmentResponseDto> CreateEquipment(EquipmentCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentService.EquipmentServiceClient(channel);
            var response = await client.CreateEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new EquipmentResponseDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description
            }); ;
        }
        
    }
}