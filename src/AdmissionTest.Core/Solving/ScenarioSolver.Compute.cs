using System;
using System.Collections.Generic;
using System.Linq;
using AdmissionTest.Core.Models;

namespace AdmissionTest.Core.Solving;

public sealed partial class ScenarioSolver
{
    //validacao dos criterios do desafio
    private partial SolverComputation ComputeBestCombination(Scenario scenario)
    {
        var seqs = scenario.Sequences;
        var n = seqs.Count;

        var mins = new int[n];
        var maxs = new int[n];
        for (int i = 0; i < n; i++)
        {
            mins[i] = seqs[i].Values.Min();
            maxs[i] = seqs[i].Values.Max();
        }

        int bestDiff = int.MaxValue;
        int bestSum  = int.MaxValue;
        int[]? bestValues = null; // valores escolhidos
        int[]? bestIdxs   = null; // índices escolhidos

        var curValues = new int[n];
        var curIdxs   = new int[n];

        void Dfs(int idx, int sumSoFar)
        {
            if (idx == n)
            {
                var diff = Math.Abs(sumSoFar - scenario.TargetValue);
                if (IsBetter(diff, sumSoFar, curValues, bestDiff, bestSum, bestValues))
                {
                    bestDiff   = diff;
                    bestSum    = sumSoFar;
                    bestValues = (int[])curValues.Clone();
                    bestIdxs   = (int[])curIdxs.Clone();
                }
                return;
            }

            int remMin = 0, remMax = 0;
            for (int k = idx; k < n; k++) { remMin += mins[k]; remMax += maxs[k]; }

            int low  = sumSoFar + remMin;
            int high = sumSoFar + remMax;

            int bestPossibleDiff =
                (low <= scenario.TargetValue && scenario.TargetValue <= high)
                ? 0
                : Math.Min(Math.Abs(low - scenario.TargetValue), Math.Abs(high - scenario.TargetValue));

            if (bestPossibleDiff > bestDiff)
                return; // não tem como melhorar o melhor diff atual

            var ordered = seqs[idx].Values
                .Select((v, j) => (v, j))
                .OrderBy(t => t.v);

            foreach (var (v, j) in ordered)
            {
                curValues[idx] = v;
                curIdxs[idx]   = j;
                Dfs(idx + 1, sumSoFar + v);
            }
        }

        Dfs(0, 0);

        if (bestIdxs is null)
            throw new InvalidOperationException("No combination explored for the given scenario.");

        return new SolverComputation(bestIdxs, score: 0);

        static bool IsBetter(
            int candDiff, int candSum, int[] candVec,
            int bestDiff, int bestSum, int[]? bestVec)
        {
            if (candDiff < bestDiff) return true;
            if (candDiff > bestDiff) return false;

            if (candSum < bestSum) return true;
            if (candSum > bestSum) return false;

            if (bestVec is null) return true;

            for (int i = 0; i < candVec.Length; i++)
            {
                if (candVec[i] < bestVec[i]) return true;
                if (candVec[i] > bestVec[i]) return false;
            }
            return false; // idênticos
        }
    }
}
