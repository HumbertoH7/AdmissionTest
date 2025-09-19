using System;
using System.IO;
using AdmissionTest.Core.Parsing;
using AdmissionTest.Core.Solving;

var path = args.Length > 0 ? args[0] : Path.Combine("data", "scenarios.txt");

try
{
    var loader = new ScenarioLoader(new ScenarioFileParser());
    var scenarios = loader.LoadFromFile(path);

    var solver = new ScenarioSolver();
    foreach (var scenario in scenarios)
    {
        var result = solver.Solve(scenario);
        foreach (var line in ReportFormatter.ToConsoleLines(result))
            Console.WriteLine(line);
        Console.WriteLine();
    }
}
catch (ParseException ex)
{
    Console.Error.WriteLine($"Parsing error: {ex.Message}");
    Environment.ExitCode = 1;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Unexpected error: {ex}");
    Environment.ExitCode = 99;
}
