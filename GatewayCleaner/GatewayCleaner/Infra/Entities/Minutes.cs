namespace GatewayCleaner.Infra.Entities;

internal class Minutes
{
    public int ToDecrease { get; private set; }
    public int BetweenRuns { get; private set; }

    public Minutes(int toDecrease, int betweenRuns)
    {
        ToDecrease = toDecrease;
        BetweenRuns = betweenRuns;
    }
}
