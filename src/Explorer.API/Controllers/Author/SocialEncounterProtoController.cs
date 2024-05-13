using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using System;

namespace Explorer.API.Controllers.Author
{
    public class SocialEncounterProtoController : Encounter.EncounterBase
    {
        private readonly ILogger<SocialEncounterProtoController> _logger;

        public SocialEncounterProtoController(ILogger<SocialEncounterProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<EncounterResponseDto> CreateSocialEncounter(SocialEncounterCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CreateSocialEncounterAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new EncounterResponseDto
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                Picture = response.Picture,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Radius = response.Radius,
                XpReward = response.XpReward,
                Status=response.Status,
                Type = response.Type,
            });

    }
    }
}
