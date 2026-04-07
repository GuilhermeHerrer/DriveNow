using AutoMapper;
using DriveNowApi.Data;
using DriveNowApi.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using DriveNowApi.Service;

using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ClienteProfile>();
    cfg.AddProfile<AgenciaProfile>();
    cfg.AddProfile<VeiculoProfile>();
    cfg.AddProfile<LocacaoProfile>();
});

builder.Services.AddHttpClient<ViaCepService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("DriveNow API").WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
