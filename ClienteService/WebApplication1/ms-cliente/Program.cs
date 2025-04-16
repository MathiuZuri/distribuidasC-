using Microsoft.EntityFrameworkCore;
using ms_cliente.data;
using ms_cliente.repository;
using ms_cliente.service;
using ms_cliente.service.impl;
using ms_cliente.util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Dependency Injection
builder.Services.AddScoped<IClienteRepository, ClienteRepository>(); // Corregido aquí
builder.Services.AddScoped<IClienteService, ClienteServiceImpl>();

// Configure Seeder
builder.Services.AddHostedService<ClienteSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();