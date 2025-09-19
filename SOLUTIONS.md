# SOLUTIONS (Checklist)

## Objetivo - Implementacao de parsing e solver (criterio de desempate)
1 - Minimizar | sum - target|
2 - Em caso de empate preferir a soma menor
3 - Persistindo empate, vetor lexocograficamente menor 

## Plano de Commits - Etapas 
1 - Add arquivo SOLUTIONS.md
2 - Editando Parsing
3 - Editando Solver
4 - Return do App
5 - Documentacao 

## Historico de Commits efetuados
1. add SOLUTIONS.md
2. editar Scenario  
3. add TextExtensions 
4. add ParseException 
5. editar static ScenarioParser.ParseBlock e add ScenarioFileParser : IScenarioParser
6. add ScenarioLoader 
7. add SolverResult
8. add ScenarioSolver 
9. implementacao ComputeBestCombination e SolverComputation
10.add ReportFormatter.ToConsoleLines e AdmissionTest.App/Program.cs
11. editar SOLUTIONS.md

## Alteracoes Tecnicas 
1 - Target Framework  - Net8.0
2 - Ignorar # e linhas em branco
3 - Validacao para favorecer desempate, descrito na regra de negocio
4 - Validacao de formatacao para facilitar as comparacoes nos cenarios informados

## END SOLUTIONS ##
