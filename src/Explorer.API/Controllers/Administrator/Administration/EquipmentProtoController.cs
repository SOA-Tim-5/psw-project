using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Administrator.Administration
{
    public class EquipmentProtoController : EquipmentService.EquipmentServiceBase
    {
        private readonly ILogger<EquipmentProtoController> _logger;

        public EquipmentProtoController(ILogger<EquipmentProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<EquipmentResponseDto> Create(EquipmentCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentService.EquipmentServiceClient(channel);
            var response = await client.CreateAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new EquipmentResponseDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description
            }); ;
        }
        public async Task<List<EquipmentResponseDto>> GetAll(ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentService.EquipmentServiceClient(channel);
            var response = await client.GetAllAsync(new Empty());

            List<EquipmentResponseDto> rs = new List<EquipmentResponseDto>();
            rs.AddRange(response.EquipmentResponses);

            return await Task.FromResult(rs);
        }
    }
}
