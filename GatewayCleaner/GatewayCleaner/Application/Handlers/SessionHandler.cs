using GatewayCleaner.Domain.Services;
using GatewayCleaner.Infra.Entities;
using Serilog;

namespace GatewayCleaner.Application.Handlers;

internal class SessionHandler
{
    private readonly string _connectionString;
    private readonly int _minutesToDecrease;

    public SessionHandler(string connectionString, int minutesToDecrease)
    {
        _connectionString = connectionString;
        _minutesToDecrease = minutesToDecrease;
    }

    public void Run()
    {
        try
        {
            var service = new SessionService(_connectionString);

            if (!service.ValidateConnection()) return;

            List<Session> sessions = service.GetSessions(_minutesToDecrease);

            service.Kill(sessions);
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Erro ao executar processo principal.");
        }
    }
}
