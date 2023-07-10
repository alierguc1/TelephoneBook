using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using TelephoneBook.Report.Business.IoC;
using TelephoneBook.Report.API.IoC;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessDependencies();
builder.ConfigureRabbitMQ();
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
app.UseHealthChecks("/report-service-health", new HealthCheckOptions
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
