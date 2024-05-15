using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Author
{
    public class FacilityProtoController : FacilityService.FacilityServiceBase
    {
        private readonly ILogger<FacilityProtoController> _logger;

        public FacilityProtoController(ILogger<FacilityProtoController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = "authorPolicy")]
        public override async Task<FacilityResponseDto> CreateFacility(FacilityCreateDto message,
                    ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new FacilityService.FacilityServiceClient(channel);
            var response = await client.CreateFacilityAsync(message);

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
            });
        }
        [Authorize(Policy = "authorPolicy")]
        public override async Task<FacilityListResponse> GetByAuthorId(GetFacilityParams message, ServerCallContext context)
        {

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new FacilityService.FacilityServiceClient(channel);
            var response = await client.GetByAuthorIdAsync(message);

            List<FacilityResponseDto> rs = new List<FacilityResponseDto>();

            foreach (FacilityResponseDto tr in response.FacilityResponses)
            {

                rs.Add(new FacilityResponseDto
                {
                    Id = tr.Id,
                    Name = tr.Name,
                    Description = tr.Description,
                    ImagePath = tr.ImagePath,
                    AuthorId = tr.AuthorId,
                    Category = tr.Category,
                    Longitude = tr.Longitude,
                    Latitude = tr.Latitude
                });
            }

            return await Task.FromResult(response);

        }

    }
}
