using System;
using System.Collections.Generic;
using System.Linq;
using AdmissionTest.Core.Models;

namespace AdmissionTest.Core.Parsing;

public interface IScenarioParser
{
    IReadOnlyList<Scenario> Parse(IReadOnlyList<string> lines);
}

public sealed class ScenarioFileParser : IScenarioParser
{
    public IReadOnlyList<Scenario> Parse(IReadOnlyList<string> lines)
    {
        if (lines is null || lines.Count == 0)
            throw new ParseException("Input is empty.");

        var cleaned = lines
            .Select((t, i) => (text: t, lineNo: i + 1))
            .Where(x => !x.text.IsCommentOrBlank())
            .ToList();

        if (cleaned.Count == 0)
            throw new ParseException("No scenarios found (only comments/blank lines).");

        var blocks = new List<(int headerLine, List<string> texts)>();

        for (int i = 0; i < cleaned.Count;)
        {
            if (!cleaned[i].text.Contains("->"))
                throw new ParseException("Expected scenario header '<Name> -> <target>'.", cleaned[i].lineNo);

            var headerLineNo = cleaned[i].lineNo;
            var texts = new List<string> { cleaned[i].text };
            i++;

            while (i < cleaned.Count && !cleaned[i].text.Contains("->"))
            {
                texts.Add(cleaned[i].text);
                i++;
            }

            if (texts.Count == 1)
                throw new ParseException("Scenario has no sequences.", headerLineNo);

            blocks.Add((headerLineNo, texts));
        }

        var scenarios = new List<Scenario>(blocks.Count);

        foreach (var (headerLine, texts) in blocks)
        {
            try
            {
                var scenario = ScenarioParser.ParseBlock(texts);
                scenarios.Add(scenario);
            }
            catch (FormatException ex)
            {
                throw new ParseException(ex.Message, headerLine);
            }
            catch (ArgumentException ex)
            {
                throw new ParseException(ex.Message, headerLine);
            }
        }

        return scenarios;
    }
}
