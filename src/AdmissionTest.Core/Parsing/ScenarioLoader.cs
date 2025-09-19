using System.Collections.Generic;
using System.IO;
using AdmissionTest.Core.Models;

namespace AdmissionTest.Core.Parsing;

public interface IScenarioLoader
{
    IReadOnlyList<Scenario> LoadFromFile(string path);
}

public sealed class ScenarioLoader : IScenarioLoader
{
    private readonly IScenarioParser _parser;
    public ScenarioLoader(IScenarioParser parser) => _parser = parser;
    public IReadOnlyList<Scenario> LoadFromFile(string path) => _parser.Parse(File.ReadAllLines(path));
}
