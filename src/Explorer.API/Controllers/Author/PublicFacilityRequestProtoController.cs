using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Google.Protobuf.WellKnownTypes;

namespace Explorer.API.Controllers.Author
{
    public class PublicFacilityRequestProtoController:PublicFacilityRequestService.PublicFacilityRequestServiceBase
    {
        private readonly ILogger<PublicFacilityRequestProtoController> _logger;

        public PublicFacilityRequestProtoController(ILogger<PublicFacilityRequestProtoController> logger)
        {
            _logger = logger;
        }
        
        public override async Task<PublicFacilityRequestResponseDtoF> Create(PublicFacilityRequestCreateDtoF message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new PublicFacilityRequestService.PublicFacilityRequestServiceClient(channel);
            var response = await client.CreateAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new PublicFacilityRequestResponseDtoF
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

        public async Task<List<PublicFacilityRequestResponseDtoF>> GetAllFacilityRequest(ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new PublicFacilityRequestService.PublicFacilityRequestServiceClient(channel);
            var response = await client.GetAllFacilityRequestAsync(new Empty());

            List<PublicFacilityRequestResponseDtoF> rs = new List<PublicFacilityRequestResponseDtoF>();
            rs.AddRange(response.PublicFacilityRequestsResponses);

            return await Task.FromResult(rs);
        }
        
    }

}
