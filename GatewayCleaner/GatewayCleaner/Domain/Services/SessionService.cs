﻿using GatewayCleaner.Infra.Entities;
using GatewayCleaner.Infra.Repositories;
using Serilog;

namespace GatewayCleaner.Domain.Services;

internal class SessionService
{
    private readonly SessionRepository _sessionRepository;

    public SessionService(string connectionString)
    {
        _sessionRepository = new SessionRepository(connectionString);
    }

    public bool ValidateConnection()
    {
        try
        {
            if (_sessionRepository.ConnectionStatus())
                return true;

            Log.Error("Erro ao se conectar ao banco de dados.");
            return false;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro ao se conectar ao banco de dados.");
            return false;
        }
    }

    public List<Session> GetSessions(int minutesToDecrease)
    {
        DateTime minimumTime = DateTime.UtcNow.AddMinutes(minutesToDecrease);

        Log.Information($"Listando sessões anteriores a {minimumTime:yyyy-MM-dd HH:mm:ss}...");
        List<Session> sessions = _sessionRepository.GetSessions(minimumTime);
        Log.Information($"{sessions.Count} sessões localizadas.");

        if (sessions.Count > 0) Console.WriteLine("SPID, UserName, StartTime, CpuTime, ElapsedTime");

        foreach (var session in sessions)
            Console.WriteLine(session);

        Console.WriteLine();

        return sessions;
    }

    public void Kill(List<Session> sessions)
    {
        if (sessions.Count > 0)
        {
            Log.Information($"[{DateTime.Now:HH:mm:ss} INF] Matando as conexões");

            foreach (var session in sessions)
                Kill(session);
        }
        else
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss} INF] Não há sessões para matar!");
        }
    }

    private void Kill(Session session)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(session.SPID))
                return;

            DateTime start = DateTime.Now;
            _sessionRepository.Kill(session.SPID);
            Log.Warning($"Sessão [{session.SPID}] levou {DateTime.Now - start} para ser encerrada.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Erro ao matar a sessão [{session.SPID}].");
        }
    }
}
