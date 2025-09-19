namespace AdmissionTest.Core.Parsing;

public sealed class ParseException : Exception
{
  public int? LineNumber { get; }

  public Parseexception(string message, int? lineNumber = null)
    : base(lineNumber is null ? message : $"Line {lineNumber}: {message}")
    {
      LineNumber = lineNumber;
    }

}
