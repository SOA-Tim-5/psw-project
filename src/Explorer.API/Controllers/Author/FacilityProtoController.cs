using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Author
{
    public class FacilityProtoController : FacilityService.FacilityServiceBase
    {
        private readonly ILogger<FacilityProtoController> _logger;

        public FacilityProtoController(ILogger<FacilityProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<FacilityResponseDto> Create(FacilityCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new FacilityService.FacilityServiceClient(channel);
            var response = await client.CreateAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new FacilityResponseDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                ImagePath = response.ImagePath,
                AuthorId = response.AuthorId,
                Category = response.Category,
                Longitude = response.Longitude,
                Latitude = response.Latitude
            }); ;
        }

        public async Task<List<FacilityResponseDto>> GetByAuthorId(GetParams message, ServerCallContext context)
        {

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new FacilityService.FacilityServiceClient(channel);
            var response = await client.GetByAuthorIdAsync(message);

            List<FacilityResponseDto> rs = new List<FacilityResponseDto>();
            rs.AddRange(response.FacilityResponses);

            return await Task.FromResult(rs);
        }

        
    }
}
