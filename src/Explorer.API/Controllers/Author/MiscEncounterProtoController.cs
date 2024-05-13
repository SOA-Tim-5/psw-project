using System;
using System.Net.Security;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Author
{
    public class MiscEncounterProtoController : Encounter.EncounterBase
    {
        private readonly ILogger<MiscEncounterProtoController> _logger;

        public MiscEncounterProtoController(ILogger<MiscEncounterProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<MiscEncounterResponseDto> CreateMiscEncounter(MiscEncounterCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CreateMiscEncounterAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new MiscEncounterResponseDto
            {
                Id = response.Id,
                ChallengeDone = response.ChallengeDone,
                Title = response.Title,
                Description = response.Description,
                Picture = response.Picture,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Radius = response.Radius,
                XpReward = response.XpReward
            });    
        }
    }
}

