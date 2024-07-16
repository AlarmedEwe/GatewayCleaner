using GatewayCleaner.Infra.Entities;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;

namespace GatewayCleaner.Infra.Repositories;

internal class SessionRepository
{
    private readonly string _connectionString;

    public SessionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool ConnectionStatus()
    {
        using var conn = new AdomdConnection(_connectionString);
        conn.Open();

        var isActive = conn.State == ConnectionState.Open;
        conn.Close();

        return isActive;
    }

    public List<Session> GetSessions(DateTime minimumTime, string[] allowedUsers)
    {
        string filter = (allowedUsers.Length > 0 ? " and SESSION_USER_NAME <> '" : "")
            + string.Join("' and SESSION_USER_NAME <> '", allowedUsers)
            + (allowedUsers.Length > 0 ? "' " : "");

        string commandText = $@"
            select SESSION_SPID, SESSION_USER_NAME, SESSION_START_TIME, SESSION_CPU_TIME_MS, SESSION_ELAPSED_TIME_MS, SESSION_LAST_COMMAND
            from $System.Discover_Sessions
            where SESSION_START_TIME < '{minimumTime:yyyy-MM-dd HH:mm:ss}'
                {filter}
            order by SESSION_START_TIME
        ";

        using var conn = new AdomdConnection(_connectionString);
        conn.Open();

        var cmd = new AdomdCommand(commandText, conn);
        using AdomdDataReader dr = cmd.ExecuteReader();

        var result = new List<Session>();

        while (dr.Read())
        {
            result.Add(new Session(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]));
        }

        return result;
    }

    public bool Kill(string spid)
    {
        using var conn = new AdomdConnection(_connectionString);
        conn.Open();

        var minimumTime = DateTime.UtcNow.AddMinutes(-30);

        string commandText = $@"
                <Cancel xmlns=""http://schemas.microsoft.com/analysisservices/2003/engine"">
	                <SPID>{spid}</SPID>
	                <CancelAssociated>1</CancelAssociated>
                </Cancel>
            ";

        var cmd = new AdomdCommand(commandText, conn);
        int affectedRows = cmd.ExecuteNonQuery();

        return affectedRows > 0;
    }
}
