namespace AdmissionTest.Core.Models;

public sealed record Scenario
{
    public required string Name { get; init; }
    public required int TargetValue { get; init; }
    public required IReadOnlyList<Sequence> Sequences { get; init; }

    public override string ToString() => $"{Name} (target: {TargetValue})";
}
