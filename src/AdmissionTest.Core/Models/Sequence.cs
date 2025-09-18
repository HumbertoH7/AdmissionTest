namespace AdmissionTest.Core.Models;

public sealed record Sequence
{
    public required string Label { get; init; }
    public required IReadOnlyList<int> Values { get; init; }

    public override string ToString() => $"{Label}: [{string.Join(", ", Values)}]";
}
