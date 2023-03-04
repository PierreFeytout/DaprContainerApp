namespace PublishBackend
{
    using Dapr.Client;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            var routeBuilder = app.MapPost("/messages", async (string data) =>
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