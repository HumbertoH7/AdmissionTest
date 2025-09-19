namespace AdmissionTest.Core.Parsing;

public static class TextExtensions
{
  public static (string left, string right) SplitBySeparator( this string line, string separator, bool allowEmptySides = false)
  {
    if(line is null) throw new ArgumentNullException(nameof(line));
    if(separator is null) throw new ArgumentNullException(nameof(separator));

    var ind = line.IndexOf(separator, StringComparison.Ordinal);
    if(ind < 0)
      throw new ArgumentException($"Separator '{separator}' not found in '{line}'.");

    var left = line[..ind].Trim();
    var right = line[(ind + separator.Length)..].Trim();

    if(!allowEmptySides && (left.Length == 0 right.Length == 0))
      throw new ArgumentException($"Empty side around '{separator}' in '{line}'.");

    return (left, right);
  }

  public static bool IsCommentOrBlank(this string line) => string.IsNullOrWhiteSpace(line) || line.TrimStart().StartWith("#");
}
