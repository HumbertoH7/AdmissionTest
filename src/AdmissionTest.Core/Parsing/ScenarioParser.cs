namespace AdmissionTest.Core.Parsing;

using AdmissionTest.Core.Models;

internal static class ScenarioParser
{
    public static Scenario ParseBlock(IReadOnlyList<string> block)
    {
        if (block.Count == 0)
        {
            throw new ArgumentException("Scenario block cannot be empty.", nameof(block));
        }

        var header = ParseHeader(block[0]);
        var sequences = ParseSequences(block.Skip(1));

        return new Scenario
        {
            Name = header.name,
            TargetValue = header.target,
            Sequences = sequences
        };
    }

    private static (string name, int target) ParseHeader(string raw)
    {
        var parts = raw.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new FormatException($"Invalid scenario header: '{raw}'. Expected 'Name -> Target'.");
        }

        if (!int.TryParse(parts[1], out var target))
        {
            throw new FormatException($"Invalid numeric target in scenario header: '{raw}'.");
        }

        return (parts[0], target);
    }

    private static IReadOnlyList<Sequence> ParseSequences(IEnumerable<string> rawLines)
    {
        var sequences = new List<Sequence>();

        foreach (var line in rawLines)
        {
            if (IsIgnorable(line))
            {
                continue;
            }

            var sequence = ParseSequence(line);
            sequences.Add(sequence);
        }

        return sequences;
    }

    private static Sequence ParseSequence(string raw)
    {
        var parts = raw.Split(':', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new FormatException($"Invalid sequence definition: '{raw}'. Expected 'Label: numbers...'.");
        }

        var numbers = ParseNumbers(parts[1]);
        return new Sequence
        {
            Label = parts[0],
            Values = numbers
        };
    }

    private static IReadOnlyList<int> ParseNumbers(string raw)
    {
        return raw
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(static token => int.Parse(token, provider: null))
            .ToList();
    }

    private static bool IsIgnorable(string line)
    {
        var trimmed = line.Trim();
        return trimmed.Length == 0 || trimmed.StartsWith('#');
    }
}
