using Microsoft.EntityFrameworkCore;
using Portal.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//load environment variables
var connectionString = builder.Configuration
    .GetConnectionString("PortalConnectionString");

builder.Services.AddDbContext<PortalContext>(options =>
options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapStaticAssets();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(_ => _.Servers = []);
}

app.Run();
