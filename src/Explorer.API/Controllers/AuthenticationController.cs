﻿using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentResults;
using System.Text.Json;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    //private readonly IAuthenticationService _authenticationService;
    private readonly IWalletService _walletService;
    static readonly HttpClient client = new HttpClient();

    public AuthenticationController(IWalletService walletService)
    {
        //_authenticationService = authenticationService;
        _walletService = walletService;
    }

   /* [HttpPost]
    public ActionResult<RegistrationConfirmationTokenDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        if(result.IsSuccess && !result.IsFailed)
        {
            _walletService.Create(new Payments.API.Dtos.WalletCreateDto(result.Value.Id));
        }
        return CreateResponse(result);
    }*/

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationTokensDto>> Login([FromBody] CredentialsDto credentials)
    {
      
        using StringContent jsonContent = new(JsonSerializer.Serialize(credentials), Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PostAsync("https://localhost:44332/api/users/login", jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return CreateResponse(jsonResponse.ToResult());
        //var result = _authenticationService.Login(credentials);
        //return CreateResponse(result);
    }

    //[HttpPost("reset-password")]
    //public ActionResult<ResetPasswordTokenDto> GenerateResetPasswordLink([FromBody] ResetPasswordEmailDto resetPasswordEmail)
    //{
    //    var result = _authenticationService.GenerateResetPasswordToken(resetPasswordEmail);
    //    return CreateResponse(result);
    //}

    //[HttpPatch("reset-password/new")]
    //public ActionResult ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    //{
    //    string email = ExtractPayload(resetPasswordRequestDto.Token);
    //    var result = _authenticationService.ResetPassword(resetPasswordRequestDto, email);
    //    return Ok();
    //}

    //private string ExtractPayload(string token)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();

    //    // Read token without validation
    //    var jwtToken = tokenHandler.ReadJwtToken(token);

    //    // Access claims directly from the token
    //    var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
    //    return emailClaim;
    //}

    //[HttpGet("confirm-registration")]
    //public ActionResult ConfirmPassword([FromQuery] string confirm_registration_token)
    //{

    //    var tokenHandler = new JwtSecurityTokenHandler();

    //    var jwtToken = tokenHandler.ReadJwtToken(confirm_registration_token);

    //    var usermname = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
    //    var confirm = jwtToken.Claims.FirstOrDefault(c => c.Type == "confirm")?.Value;

    //    var result = _authenticationService.ConfirmRegistration(usermname, confirm);

    //    return CreateResponse(result);
    //}
}
