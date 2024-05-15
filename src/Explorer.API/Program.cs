using Explorer.API.Controllers;
using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Author.TourAuthoring;

using Explorer.API.Controllers.Author.TourAuthoring;

using Explorer.API.Controllers.Tourist;
using Explorer.API.Startup;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddHostedService<MailingListScheduler>();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true; // Ovo omogu?ava detaljnije greške u razvojnom okruženju
}).AddJsonTranscoding();



var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage();
   // app.UseSwagger();
   // app.UseSwaggerUI();
//}
//else
//{
    //app.UseExceptionHandler("/error");
    //app.UseHsts();
//}

app.UseRouting();
app.UseCors(corsPolicy);
//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.MapGrpcService<AuthenticationProtoController>();
app.MapGrpcService<MiscEncounterProtoController>();
app.MapGrpcService<TourProtoController>();

app.MapGrpcService<FollowerProtoController>();  


app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { 
    
    
    
    }
}