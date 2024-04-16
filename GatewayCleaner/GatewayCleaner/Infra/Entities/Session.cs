namespace GatewayCleaner.Infra.Entities;

internal class Session
{
    public string? SPID { get; set; }
    public string? UserName { get; set; }
    public DateTime? StartTime { get; set; }
    public string? CpuTime { get; set; }
    public string? ElapsedTime { get; set; }
    public string? LastCommand { get; set; }

    public Session(string? spid, string? userName, DateTime? startTime, string? cpuTime, string? elapsedTime, string? lastCommand)
    {
        SPID = spid;
        UserName = userName;
        StartTime = startTime;
        CpuTime = cpuTime;
        ElapsedTime = elapsedTime;
        LastCommand = lastCommand;
    }

    public Session(object? spid, object? userName, object? startTime, object? cpuTime, object? elapsedTime, object? lastCommand)
    {
        SPID = Convert.ToString(spid);
        UserName = Convert.ToString(userName);
        StartTime = Convert.ToDateTime(startTime);
        CpuTime = Convert.ToString(cpuTime);
        ElapsedTime = Convert.ToString(elapsedTime);
        LastCommand = Convert.ToString(lastCommand);
    }

    public override string ToString()
        => $"{SPID}, {UserName}, {StartTime}, {CpuTime}, {ElapsedTime}";
}
