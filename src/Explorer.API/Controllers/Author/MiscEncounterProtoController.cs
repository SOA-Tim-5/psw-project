﻿using System;
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

        public override async Task<EncounterResponseDto> CreateSocialEncounter(SocialEncounterCreateDto request,
    ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Encounter.EncounterClient(channel);
            var response = await client.CreateSocialEncounterAsync(request);

            // Console.WriteLine(response.AccessToken);

            return new EncounterResponseDto
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
            };
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

