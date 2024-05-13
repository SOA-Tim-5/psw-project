using Explorer.API.Controllers.Author;
using Explorer.Stakeholders.Core.Domain;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Tourist
{
    public class EncounterControllerProto : Encounter.EncounterBase
    {
        private readonly ILogger<EncounterControllerProto> _logger;

        public EncounterControllerProto(ILogger<EncounterControllerProto> logger)
        {
            _logger = logger;
        }

        public override async Task<EncounterResponseDto> Activate(TouristPosition message,
    ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.ActivateAsync(message);

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
                Status = response.Status,
                Type = response.Type,
            });
        }

        public override async Task<EncounterInstanceResponseDto> FindEncounterInstance(EncounterInstanceId message,
    ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.FindEncounterInstanceAsync(message);

            return await Task.FromResult(new EncounterInstanceResponseDto
            {
              UserId= response.UserId,
              Status = response.Status,
              CompletitionTime=response.CompletitionTime

            });
        }

        public override async Task<TouristProgress> CompleteMisc(EncounterInstanceId message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CompleteMiscAsync(message);

            return await Task.FromResult(new TouristProgress
            {
              Xp=response.Xp,
              Level=response.Level,
            });
        }

        public override async Task<Inrange> CompleteHiddenLocationEncounter(TouristPosition message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CompleteHiddenLocationEncounterAsync(message);

            return await Task.FromResult(new Inrange
            {
              In=true
            });
        }
        public override async Task<TouristProgress> FindTouristProgressByTouristId(TouristId message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.FindTouristProgressByTouristIdAsync(message);

            return await Task.FromResult(new TouristProgress
            {
                Xp = response.Xp,
                Level = response.Level,
            });
        }

        public override async Task<HiddenLocationEncounterResponseDto> FindHiddenLocationEncounterById(EncounterId message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.FindHiddenLocationEncounterByIdAsync(message);
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
        public override async Task<Inrange> IsUserInCompletitionRange(Position message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.IsUserInCompletitionRangeAsync(message);
            return await Task.FromResult(new Inrange
            {
                In = true
            }); 
        }

        public override async Task<ListEncounterResponseDto> FindAll(Inrange message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.FindAllAsync(message);
            return await Task.FromResult(response); 
        }
        public override async Task<ListEncounterResponseDto> FindAllInRangeOf (AllInRange message,
ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.FindAllInRangeOfAsync(message);
            return await Task.FromResult(response);
        }
    }
}
