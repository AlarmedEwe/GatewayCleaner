namespace GatewayCleaner.Infra.Entities;

internal class Runtime
{
    public TimeSpan Begin { get; private set; }
    public TimeSpan End { get; private set; }

    public Runtime(string begin, string end)
    {
        Begin = TimeOnly.Parse(begin).ToTimeSpan();
        End = TimeOnly.Parse(end).ToTimeSpan();
    }
}