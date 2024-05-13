using Explorer.API.Controllers;
using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.API.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddHostedService<MailingListScheduler>();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();

builder.Services.AddGrpc().AddJsonTranscoding();

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
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
//app.MapGrpcService<AuthenticationProtoController>();
app.MapGrpcService<MiscEncounterProtoController>();
app.MapGrpcService<SocialEncounterProtoController>();
app.MapGrpcService<HiddenLocationEncounterProtoController>();
app.MapGrpcService<EncounterControllerProto>();

app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { }
}