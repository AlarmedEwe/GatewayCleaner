using GatewayCleaner.Application.Handlers;
using Microsoft.Extensions.Configuration;
using Serilog;

Console.Title = "Gateway Cleaner - v1.1";

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
    var connectionString = config.GetConnectionString("SSAS")
        ?? throw new Exception("Erro ao recuperar a Connection String");
    int minutesToDecrease = Convert.ToInt32(config["minutes:toDecrease"]);
    int minutesBetweenRuns = Convert.ToInt32(config["minutes:betweenRuns"]);

    var handler = new SessionHandler(connectionString, minutesToDecrease);

    while (true)
    {
        handler.Run();
        Thread.Sleep(minutesBetweenRuns * 60 * 1000); // Wait for 1 min and run again
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