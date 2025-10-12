using Microsoft.AspNetCore.Mvc;
using Server.Response;

var builder = WebApplication.CreateBuilder(args);

var port = builder.Configuration.GetValue<int>("Port") == 0 ? 5000 : builder.Configuration.GetValue<int>("Port");
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// override the default model state invalid response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorMessages = context.ModelState
            .Where(ms => ms.Value?.Errors.Any() == true)
            .SelectMany(ms => ms.Value!.Errors)
            .Select(e => e.ErrorMessage ?? "Unknown error");
        var errorMessage = string.Join("; ", errorMessages);
        return new BadRequestObjectResult(new Response<object>
        {
            StatusCode = 400,
            Ok = false,
            Data = null,
            Error = errorMessage
        });
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
