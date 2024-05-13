using System;
using System.Net.Security;
using FluentResults;
using System.Security.Claims;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.Domain;
using System.Collections;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Explorer.API.Controllers.Author.TourAuthoring
{
    public class TourProtoController : TourService.TourServiceBase
    {
        private readonly ILogger<TourProtoController> _logger;

        public TourProtoController(ILogger<TourProtoController> logger)
        {
            _logger = logger;
        }
        
        public override async Task<TourResponseDto> Create(TourCreateDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.CreateAsync(message);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new TourResponseDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                AuthorId = response.AuthorId,
                Category = response.Category,
                Status = response.Status,
                Difficulty = response.Difficulty,
                Tags = { response.Tags },
                Price = response.Price,
                IsDeleted = response.IsDeleted
            });
        }
        
        public override async Task<TourListResponse> GetAuthorsTours(GetParams message, ServerCallContext context)
        {
           
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.GetAuthorsToursAsync(message);

            List<TourResponseDto> rs = new List<TourResponseDto>();
            
            foreach(TourResponseDto tr in response.TourResponses)
            {
                
                rs.Add(new TourResponseDto
                {
                    Id = tr.Id,
                    Name = tr.Name,
                    Description = tr.Description,
                    AuthorId = tr.AuthorId,
                    Category = tr.Category,
                    Status = tr.Status,
                    Difficulty = tr.Difficulty,
                    Tags = { tr.Tags },
                    Price = tr.Price,
                    IsDeleted = tr.IsDeleted
                });
            }


            return await Task.FromResult(response);
        }
        
        public override async Task<KeyPointResponseDto> CreateKeyPoint(KeyPointCreateDto message, ServerCallContext context)
        {

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.CreateKeyPointAsync(message);

            return await Task.FromResult(response);
        }

        public override async Task<TourResponseDto> GetById(GetParams message, ServerCallContext context)
        {

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.GetByIdAsync(message);

            return await Task.FromResult(new TourResponseDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                AuthorId = response.AuthorId,
                Category = response.Category,
                Status = response.Status,
                Difficulty = response.Difficulty,
                Tags = { response.Tags },
                Price = response.Price,
                IsDeleted = response.IsDeleted
            });
        }

        public override async Task<KeyPointResponseDto> GetKeyPoints(GetParams message, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.GetKeyPointsAsync(message);

            return await Task.FromResult(new KeyPointResponseDto
            {
                Id = response.Id,
                TourId = response.TourId,
                Name = response.Name,
                Description = response.Description,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                LocationAddress = response.LocationAddress,
                ImagePath = response.ImagePath,
                Order = response.Order,
            });
        }
    }
}
