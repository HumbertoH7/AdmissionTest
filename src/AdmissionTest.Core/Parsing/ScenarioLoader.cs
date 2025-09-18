namespace AdmissionTest.Core.Parsing;

using AdmissionTest.Core.Models;
using System.Collections.Immutable;

public static class ScenarioLoader
{
    public static ImmutableArray<Scenario> FromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Could not locate scenario definition at '{path}'.");
        }

        var raw = File.ReadAllLines(path);
        if (raw.Length == 0)
        {
            return ImmutableArray<Scenario>.Empty;
        }

        var builder = ImmutableArray.CreateBuilder<Scenario>();

        foreach (var block in raw.SplitBySeparator(string.IsNullOrWhiteSpace))
        {
            var scenario = ScenarioParser.ParseBlock(block);
            builder.Add(scenario);
        }

        return builder.MoveToImmutable();
    }
}
