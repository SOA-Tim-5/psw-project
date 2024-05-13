using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using System;

namespace Explorer.API.Controllers.Author
{
    public class HiddenLocationEncounterProtoController : Encounter.EncounterBase
    {
        private readonly ILogger<HiddenLocationEncounterProtoController> _logger;

        public HiddenLocationEncounterProtoController(ILogger<HiddenLocationEncounterProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<HiddenLocationEncounterResponseDto> CreateHiddenLocationEncounter(HiddenLocationEncounterCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CreateHiddenLocationEncounterAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new HiddenLocationEncounterResponseDto
            {
                Id = response.Id,
                PictureLongitude = response.PictureLongitude,
                PictureLatitude = response.PictureLatitude,
                Title = response.Title,
                Description = response.Description,
                Picture = response.Picture,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Radius = response.Radius,
                XpReward = response.XpReward,
                Status = response.Status,
            });
        }
    }
}
