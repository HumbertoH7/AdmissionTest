using System;
using System.Collections.Generic;
using System.Linq;
using AdmissionTest.Core.Models;

namespace AdmissionTest.Core.Solving;

public sealed partial class ScenarioSolver
{
    public IReadOnlyList<SolverResult> SolveAll(IEnumerable<Scenario> scenarios)
    {
        if (scenarios is null)
        {
            throw new ArgumentNullException(nameof(scenarios));
        }

        return scenarios.Select(Solve).ToList();
    }

    public SolverResult Solve(Scenario scenario)
    {
        ValidateScenario(scenario);

        var computation = ComputeBestCombination(scenario);
        var chosenValues = BuildChosenValues(scenario, computation.Indexes);
        var achievedSum = chosenValues.Sum();

        return new SolverResult
        {
            Scenario = scenario,
            ChosenValues = chosenValues,
            AchievedSum = achievedSum,
            DifferenceFromTarget = Math.Abs(achievedSum - scenario.TargetValue)
        };
    }

    private static void ValidateScenario(Scenario scenario)
    {
        if (scenario.Sequences.Count == 0)
        {
            throw new InvalidOperationException("A scenario must contain at least one sequence.");
        }

        if (scenario.Sequences.Any(sequence => sequence.Values.Count == 0))
        {
            throw new InvalidOperationException("Sequences cannot be empty.");
        }
    }

    private static IReadOnlyList<int> BuildChosenValues(Scenario scenario, IReadOnlyList<int> indexes)
    {
        if (scenario.Sequences.Count != indexes.Count)
        {
            throw new InvalidOperationException("Sequence/index mismatch.");
        }

        var values = new List<int>(indexes.Count);

        for (var i = 0; i < indexes.Count; i++)
        {
            var index = indexes[i];
            var sequence = scenario.Sequences[i];

            values.Add(sequence.Values[index]);
        }

        return values;
    }

    private partial SolverComputation ComputeBestCombination(Scenario scenario);
}
