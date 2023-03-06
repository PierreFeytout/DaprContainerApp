using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddDapr();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(builder => builder
                 .AddService(serviceName: "mybackend"))
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCloudEvents();
                app.MapControllers();
                app.MapSubscribeHandler();
            }

            app.UseAuthorization();

            //app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}