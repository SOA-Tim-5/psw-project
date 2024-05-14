using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    public class FollowerProtoController : Follower.FollowerBase
    {
        private readonly ILogger<FollowerProtoController> _logger;

        public FollowerProtoController(ILogger<FollowerProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<FollowerResponseDto> CreateNewFollowing(UserFollowingDto following,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Follower.FollowerClient(channel);
            var response = await client.CreateNewFollowingAsync(following);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new FollowerResponseDto
            {
                Id = response.Id,
                UserId = response.UserId,
                FollowedById = response.FollowedById
            });
        }
    

    public override async Task<ListFollowingResponseDto> GetFollowerRecommendations(id id,
           ServerCallContext context)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Follower.FollowerClient(channel);
        var response = await client.GetFollowerRecommendationsAsync(id);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new ListFollowingResponseDto
            {
               ResponseList = { response.ResponseList }
            }) ;
    }


        public override async Task<ListFollowingResponseDto> GetFollowings(id id,
               ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Follower.FollowerClient(channel);
            var response = await client.GetFollowingsAsync(id);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new ListFollowingResponseDto
            {
                ResponseList = { response.ResponseList }
            });
        }

        public override async Task<ListFollowingResponseDto> GetFollowers(id id,
               ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Follower.FollowerClient(channel);
            var response = await client.GetFollowersAsync(id);

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new ListFollowingResponseDto
            {
                ResponseList = { response.ResponseList }
            });
        }
    }


}

