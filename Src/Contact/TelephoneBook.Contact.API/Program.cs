using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Reflection;
using TelephoneBook.Contact.Infrastructure.IoC;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecks()
    .AddMongoDb(
    mongodbConnectionString: "mongodb://localhost:27017",
    name: "Mongo Db Check",
    failureStatus: HealthStatus.Unhealthy | HealthStatus.Healthy,
    tags: new string[] { "mongodb" })
    .AddRabbitMQ(
    rabbitConnectionString: "amqp://guest:guest@localhost:15762",
    name: "RabbitMQ Check",
    failureStatus: HealthStatus.Unhealthy | HealthStatus.Healthy,
    tags: new string[] { "rabbitmq" });

builder.Services.AddControllers();
//builder.Services.AddHealthChecks();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureDependencies();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});
// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseHealthChecks("/contact-service-health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
