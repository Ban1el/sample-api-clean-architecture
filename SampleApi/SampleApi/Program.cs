using SampleApi.Application;
using SampleApi.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Local"))
{
    builder.Configuration.AddJsonFile(
        "appsettings.Local.json",
        optional: false,
        reloadOnChange: true
    );
}

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local"))
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();