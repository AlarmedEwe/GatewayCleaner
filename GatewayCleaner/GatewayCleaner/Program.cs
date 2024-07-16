using GatewayCleaner.Application.Configuration;
using GatewayCleaner.Application.Handlers;
using Microsoft.Extensions.Configuration;
using Serilog;

Console.Title = "Gateway Cleaner - v1.2";

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
   .AddEnvironmentVariables();

IConfiguration config = builder.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

Log.Information("Iniciando sistema...");

try
{
    var appSettings = new AppSettings(config);
    var handler = new SessionHandler(appSettings);

    while (true)
    {
        bool timeToRun = false;
        var now = DateTime.Now.TimeOfDay;
        foreach (var runtime in appSettings.Runtimes)
        {
            if (runtime.Begin < now && now < runtime.End)
            {
                timeToRun = true;
                break;
            }
        }

        if (!timeToRun) continue;

        handler.Run();
        Thread.Sleep(appSettings.Minutes.BetweenRuns * 60 * 1000); // Wait for 1 min and run again
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro na base do sistema.");
}
finally
{
    Log.CloseAndFlush();
}