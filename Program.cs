using Infra;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var envConnectionString = Environment.GetEnvironmentVariable("DefaultConnection");
    var envJwtKey = Environment.GetEnvironmentVariable("JwtKey");

    if (!string.IsNullOrEmpty(envConnectionString))
    {
        builder.Configuration["ConnectionStrings:DefaultConnection"] = envConnectionString;
    }
    if (!string.IsNullOrEmpty(envJwtKey))
    {
        builder.Configuration["Jwt:Key"] = envJwtKey;
    }
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd",
        policy => {
        policy.WithOrigins("https://painel.meunegociosimples.net")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fisio API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    app.UseCors(policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowFrontEnd");

app.MapControllers();

app.Run();
