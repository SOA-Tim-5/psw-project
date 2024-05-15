using System;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    public class AuthenticationProtoController : Authorize.AuthorizeBase
    {
        private readonly ILogger<AuthenticationProtoController> _logger;

        public AuthenticationProtoController(ILogger<AuthenticationProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<AuthenticationTokens> Authorize(Credentials request,
    ServerCallContext context)
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var channel = GrpcChannel.ForAddress("https://localhost:44332", new GrpcChannelOptions { HttpHandler = httpHandler });
                //Console.WriteLine(channel);
                var client = new Authorize.AuthorizeClient(channel);
                var response = await client.AuthorizeAsync(request);

                //Console.WriteLine(response.AccessToken);

                return await Task.FromResult(new AuthenticationTokens
                {
                    Id = response.Id,
                    AccessToken = response.AccessToken
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom uspostavljanja veze: {ex.Message}");
                throw; 
            }
        }
        
        public override async Task<ListUserResponseDtoA> GetSearch(SearchUsernameA id,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:44332", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Authorize.AuthorizeClient(channel);
            var response = await client.GetSearchAsync(id);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new ListUserResponseDtoA
            {
                ResponseList = { response.ResponseList }
            });
        }
        
        public override async Task<PersonResponseDtoA> GetByUserId(UserId userId, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://host.docker.internal:44332", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Authorize.AuthorizeClient(channel);
            var response = await client.GetByUserIdAsync(userId);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new PersonResponseDtoA
            {
                Id = response.Id,
                UserId = response.UserId,
                Name = response.Name,
                Surname = response.Surname,
                Email = response.Email,
                User = response.User,
                Bio = response.Bio,
                Motto = response.Motto,

        });
        }
        
    }
}
