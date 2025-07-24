using Api.ApiExtensions;
using Api.Mappings;
using Api.Profile;
using AutoMapper;
using Dal;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic.Infrastructure;
using Logic.Managers;
using Logic.Managers.Interfaces; // сервисы Logic
using Logic.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // MappingProfile

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrfuDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// 2) Repositories (DAL)
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInstituteRepository, InstituteRepository>();
builder.Services.AddScoped<IHeadRepository, HeadRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();

// 3) Business services (Logic)
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IInstituteService, InstituteService>();
builder.Services.AddScoped<IHeadService, HeadService>();

// 4) Utilities
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddAuthorization();


// 3) AutoMapper: сканируем профиль из Logic
builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

builder.Services.AddApiAuthentication("secretKeySecretKeySecretKey123456789");

// 4) Swagger + Controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// 5) Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();