namespace AdmissionTest.Core.Solving;

using AdmissionTest.Core.Models;

public sealed record SolverResult
{
    public required Scenario Scenario { get; init; }
    public required IReadOnlyList<int> ChosenValues { get; init; }
    public required int AchievedSum { get; init; }
    public required int DifferenceFromTarget { get; init; }
}
