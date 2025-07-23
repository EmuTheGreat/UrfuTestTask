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

// 1) EF Core + Postgres
builder.Services.AddDbContext<UrfuDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// 2) Сервисы бизнес-логики
builder.Services.AddScoped<IProgramService, ProgramService>();
//builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 3) AutoMapper: сканируем профиль из Logic
builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly,
    typeof(MappingProfile     ).Assembly);

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

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapControllers();
app.Run();