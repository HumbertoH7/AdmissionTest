namespace AdmissionTest.Core.Solving;

using System.Text;

public static class ReportFormatter
{
    public static IEnumerable<string> ToConsoleLines(SolverResult result)
    {
        yield return $"Scenario: {result.Scenario.Name}";
        yield return $" Target: {result.Scenario.TargetValue}";
        yield return $" Achieved: {result.AchievedSum}";
        yield return $" Difference: {result.DifferenceFromTarget}";
        yield return $" Chosen values: [{string.Join(", ", result.ChosenValues)}]";
    }
}
