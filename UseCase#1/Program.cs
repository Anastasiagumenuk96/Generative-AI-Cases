using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Models;
using UseCase_1.Filters;
using UseCase_1.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "Use Case #1", Version = "v1" }));

services.AddMediatR(Assembly.GetExecutingAssembly());

services.AddHttpClient();

services.AddScoped<ICountryFilterBuilder, CountryFilterBuilder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Use Case #1 API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();