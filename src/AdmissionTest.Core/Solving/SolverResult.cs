using System;
using System.Collections.Generic;
using AdmissionTest.Core.Models;

namespace AdmissionTest.Core.Solving;

public sealed record SolverResult
{
    public required Scenario Scenario { get; init; }
    public required IReadOnlyList<int> ChosenValues { get; init; }
    public required int AchievedSum { get; init; }
    public int DifferenceFromTarget => Math.Abs(AchievedSum - Scenario.TargetValue);

    public static SolverResult From(Scenario scenario, IReadOnlyList<int> chosenValues)
    {
        if (scenario is null) throw new ArgumentNullException(nameof(scenario));
        if (chosenValues is null) throw new ArgumentNullException(nameof(chosenValues));

        var sum = 0;
        for (int i = 0; i < chosenValues.Count; i++) sum += chosenValues[i];

        return new SolverResult
        {
            Scenario = scenario,
            ChosenValues = chosenValues,
            AchievedSum = sum
        };
    }
}
