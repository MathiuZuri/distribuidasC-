using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsConfigService.Models;
using MsConfigService.Services; // Crearás este servicio en el siguiente paso

var builder = WebApplication.CreateBuilder(args);

// Configuración básica (logging, etc.) ya está en tu appsettings.json

// Configuración para acceder al repositorio Git
builder.Services.Configure<GitConfigServerSettings>(builder.Configuration.GetSection("Spring:Cloud:Config:Server:Git"));

// Configuración para la seguridad (usuario y contraseña)
builder.Services.Configure<ConfigServerSecuritySettings>(builder.Configuration.GetSection("Spring:Security:User"));

// Registra el servicio que interactuará con Git
builder.Services.AddScoped<IGitConfigService, GitConfigService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); // Si implementas autenticación

app.MapControllers();

app.Run();