using Identity.Auth.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection =
    builder.Configuration.GetConnectionString("MRIdentity") ?? string.Empty;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();