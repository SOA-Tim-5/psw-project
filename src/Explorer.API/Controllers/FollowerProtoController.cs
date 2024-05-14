using Explorer.API.FollowerDtos;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    public class FollowerProtoController : Follower.FollowerBase
    {
        private readonly ILogger<FollowerProtoController> _logger;
        private readonly IBlogService _blogService;
        public FollowerProtoController(ILogger<FollowerProtoController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        public override async Task<FollowerResponseDto> CreateNewFollowing(GrpcServiceTranscoding.UserFollowingDto following,
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
        
        public override async Task<BlogListResponse> GetAllFromFollowingUsers(id id,
               ServerCallContext context)
        {

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Follower.FollowerClient(channel);
            var response = await client.GetAllFromFollowingUsersAsync(id);

            List<Blog.API.Dtos.FollowingResponseDto> result = new List<Blog.API.Dtos.FollowingResponseDto>();
            foreach(var f in response.Following)
                result.Add(new Blog.API.Dtos.FollowingResponseDto { Id = f.Id, Image =f.Image, 
                Username = f.Username});

            List<Blog.API.Dtos.BlogResponseDto> dtos = await _blogService.GetAllFromFollowingUsers(result);
            List<GrpcServiceTranscoding.BlogResponseDto> list = new();

            foreach(var d in dtos)
            {
                list.Add(new GrpcServiceTranscoding.BlogResponseDto { AuthorId=d.AuthorId,
                ClubId = 0, Date = d.Date.ToString(), Description = d.Description,
                DownvoteCount = d.DownvoteCount, Id = d.Id, Status = (int)d.Status, Title = d.Title});
            }

            // Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new BlogListResponse
            {
                Response = { list }
            });
        }
    }


}

