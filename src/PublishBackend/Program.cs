namespace PublishBackend
{
    using Dapr.Client;
    using DparContainer.Models;
    using OpenTelemetry.Metrics;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(builder => builder
                .AddService(serviceName: "publishbackend"))
                .WithTracing(builder =>
                {
                    builder.AddAspNetCoreInstrumentation();
                    builder.AddZipkinExporter(config =>
                    {
                        config.Endpoint = new Uri("http://host.docker.internal:19411/api/v2/spans");
                    });
                })
                .WithMetrics(builder =>
                {
                    builder.AddAspNetCoreInstrumentation();
                });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            var routeBuilder = app.MapPost("/messages", async (EventModel data) =>
            {
                var daprBuilder = new DaprClientBuilder();
                var daprClient = daprBuilder.Build();
                await daprClient.PublishEventAsync("messages-pub-sub", "messages", data);

                return Results.Ok();
            })
            .WithName("PublishMessages")
            .WithOpenApi();

            app.Run();
        }
    }
}