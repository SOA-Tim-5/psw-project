using System;
using System.Net.Security;
using FluentResults;
using System.Security.Claims;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
        
        public async Task<List<TourResponseDto>> GetAuthorsTours(GetParams message, ServerCallContext context)
        {
           
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:88", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TourService.TourServiceClient(channel);
            var response = await client.GetAuthorsToursAsync(message);

            List<TourResponseDto> rs = new List<TourResponseDto>();
            rs.AddRange(response.TourResponses);

            return await Task.FromResult(rs);
        }
    }
}
