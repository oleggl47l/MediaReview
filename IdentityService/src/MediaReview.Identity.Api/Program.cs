using System.Reflection;
using MediaReview.Identity.Api.ExceptionHandlers;
using MediaReview.Identity.Application.Extensions;
using MediaReview.Identity.Application.Identity.Queries;
using MediaReview.Identity.Application.Services;
using MediaReview.Identity.Domain.Entities;
using MediaReview.Identity.Domain.Interfaces;
using MediaReview.Identity.Infrastructure.Data;
using MediaReview.Identity.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("MRIdentity")));

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddMediatR(cfg =>
{
    cfg.Lifetime = ServiceLifetime.Scoped;
    cfg.RegisterServicesFromAssembly(typeof(LoginQuery).GetTypeInfo().Assembly);
});
builder.Services.AddScoped<IJwtGenerator, JwtTokenGenerator>();

builder.Services.AddApplication();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<UnblockUsersBackgroundService>();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseExceptionHandler();


app.Run();