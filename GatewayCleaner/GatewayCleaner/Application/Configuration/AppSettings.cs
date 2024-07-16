using GatewayCleaner.Infra.Entities;
using Microsoft.Extensions.Configuration;

namespace GatewayCleaner.Application.Configuration;

internal class AppSettings
{
    public readonly string ConnectionString;
    public readonly Runtime[] Runtimes;
    public readonly Minutes Minutes;
    public readonly string[] AllowedUsers;

    public AppSettings(IConfiguration config)
    {
        ConnectionString = config.GetConnectionString("SSAS")
            ?? throw new Exception("Erro ao recuperar a Connection String");
        Runtimes = config.GetSection("Runtime").Get<Runtime[]>() ?? Array.Empty<Runtime>();
        Minutes = config.GetSection("Minutes").Get<Minutes>() ?? new Minutes(-1, 1);
        AllowedUsers = config.GetSection("AllowedUsers").Get<string[]>() ?? Array.Empty<string>();
    }
}
