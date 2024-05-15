using Explorer.API.Controllers.Administrator.Administration;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using System;

namespace Explorer.API.Controllers.Tourist
{
    public class PreferenceProtoController : PreferenceService.PreferenceServiceBase
    {
        private readonly ILogger<PreferenceProtoController> _logger;

        public PreferenceProtoController(ILogger<PreferenceProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<PreferenceResponseDto> CreatePreference(PreferenceCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new PreferenceService.PreferenceServiceClient(channel);
            var response = await client.CreatePreferenceAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new PreferenceResponseDto
            {
                 Id=response.Id,
             UserId = response.UserId,
             DifficultyLevel = response.DifficultyLevel,
             WalkingRating = response.WalkingRating,
             CyclingRating = response.CyclingRating,
             CarRating = response.CarRating,
             BoatRating = response.BoatRating,
             SelectedTags = { response.SelectedTags}
        }); ;
        }
        public override async Task<PreferenceResponseDto> GetByTouristId(GetPreferenceParams message,ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new PreferenceService.PreferenceServiceClient(channel);
            var response = await client.GetByTouristIdAsync(message);

            return await Task.FromResult(response);
        }
    }
}
