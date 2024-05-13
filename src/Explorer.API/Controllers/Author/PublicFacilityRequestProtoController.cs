using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Author
{
    public class PublicFacilityRequestProtoController:PublicFacilityRequestService.PublicFacilityRequestServiceBase
    {
        private readonly ILogger<PublicFacilityRequestProtoController> _logger;

        public PublicFacilityRequestProtoController(ILogger<PublicFacilityRequestProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<PublicFacilityRequestResponseDto> Create(PublicFacilityRequestCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new PublicFacilityRequestService.PublicFacilityRequestServiceClient(channel);
            var response = await client.CreateAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new PublicFacilityRequestResponseDto
            {
                Id = response.Id,
                FacilityId = response.FacilityId,
                Status = response.Status,
                Comment = response.Comment,
                Created = response.Created,
                AuthorName = response.AuthorName,
                FacilityName = response.FacilityName,
                Author = response.Author
            }); ;
        }
    }
}
