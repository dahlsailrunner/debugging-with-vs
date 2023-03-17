using System.Reflection;
using System.Text.Json;
using CarvedRock.GitHub;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, loggerConfig) => {
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.WithProperty("Application", Assembly.GetExecutingAssembly().GetName().Name ?? "API")
        .Enrich.WithExceptionDetails()
        .Enrich.FromLogContext()
        //.WriteTo.Seq("http://localhost:5341")
        .WriteTo.Console()
        .WriteTo.Debug();
});
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");
app.MapPost("/github", async context =>
{
    var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
    Log.Debug($"Received GitHub event: {json}");
    // TODO: add security handling here based on github secrets doc
    // https://docs.github.com/en/webhooks-and-events/webhooks/securing-your-webhooks
    try
    {
        var githubEvent = JsonSerializer.Deserialize<GitHubEvent>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        });
        Log.Information("Success for event: {event}", githubEvent);
        // Could add more logic to process the event here...
        context.Response.StatusCode = 200;
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Something failed handling event! {RequestBody}", json);
        context.Response.StatusCode = 500;
    }
});
app.Run();
