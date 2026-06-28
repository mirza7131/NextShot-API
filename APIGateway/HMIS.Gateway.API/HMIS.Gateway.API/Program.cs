using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("ocelot.json")
    .AddJsonFile("ocelot.auth.json")
    .AddEnvironmentVariables()
    ;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
           //(o) =>
           //{
           //    o.GenerateDocsForGatewayItSelf = false;
           //});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerForOcelotUI();

app.UseHttpsRedirection();

await app.UseOcelot();

app.UseAuthorization();

app.MapControllers();

app.Run();
