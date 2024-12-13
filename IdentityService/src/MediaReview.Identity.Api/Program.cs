using System.Reflection;
using MediaReview.Identity.Application.Extensions;
using MediaReview.Identity.Application.Identity.Commands.User;
using MediaReview.Identity.Application.Identity.Queries;
using MediaReview.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("MRIdentity")));
// builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();