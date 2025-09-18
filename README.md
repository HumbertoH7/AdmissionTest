# Admission Test

Bem-vindo(a)! Este desafio em .NET foi preparado para avaliar como você lida com análise, debugging e tomada de decisão orientada a resultados. O objetivo não é apenas "fazer compilar", mas demonstrar raciocínio estruturado e escolhas técnicas conscientes.

## Contexto

A aplicação modela um problema de planejamento combinatório. Para cada cenário, você recebe várias sequências de inteiros e um valor alvo. O solver deve escolher **exatamente um número por sequência** de modo que a soma final fique o mais próxima possível do alvo. Caso mais de uma combinação tenha a mesma proximidade, aplicamos os seguintes critérios de desempate:

1. Preferir a soma mais baixa.
2. Persistindo o empate, escolher o vetor de valores lexicograficamente menor (comparando sequência a sequência na ordem em que aparecem no arquivo).

Os cenários são carregados a partir de `data/scenarios.txt`. Cada bloco contém um cabeçalho e N linhas de sequências, separados por linhas em branco. Comentários começam com `#` e devem ser ignorados.

### Exemplo de entrada

```
WarehouseLoad -> 47
Alpha: 12 9 5 8
Beta: 7 6 15
Gamma: 11 13 4 18

TransportPuzzle -> 63
North: 5 21 16 9
South: 8 14 11
East: 7 19 23
West: 6 5 17 18
```

### Saída esperada após implementar o solver

```
Scenario: WarehouseLoad
 Target: 47
 Achieved: 45
 Difference: 2
 Chosen values: [12, 15, 18]

Scenario: TransportPuzzle
 Target: 63
 Achieved: 63
 Difference: 0
 Chosen values: [9, 14, 23, 17]
```

O arquivo inclui outros cenários (como `LegacyMigration`) para validar decisões de parsing e de algoritmo.

## O que já existe

- `AdmissionTest.App`: aplicação console que lê os cenários e imprime um relatório.
- `AdmissionTest.Core`: camada de domínio com modelos, parser e solver.
- `ScenarioLoader` e `ScenarioParser`: início de um pipeline de parsing.
- `ScenarioSolver`: estrutura para resolver cada cenário (ainda incompleta).
- `ReportFormatter`: prepara a saída para o console.

## O que você precisa fazer

1. **Fazer o projeto compilar**
   - Há referências a métodos/extensões que não existem (`SplitBySeparator`, implementação parcial do solver, etc.).
   - Há tipos que exigem decisões (por exemplo, `ImmutableArray` requer referência explícita ou pode ser substituído por outra coleção).
   - Não remova a lógica existente sem considerar o impacto; a intenção é observar como você reconstroi as peças faltantes.

2. **Implementar o parsing corretamente**
   - Garanta que comentários e linhas em branco sejam ignorados.
   - Valide os formatos e retorne mensagens claras quando encontrar problemas.

3. **Implementar o algoritmo de seleção**
   - Escolha uma combinação por cenário que minimize `|soma - alvo|`.
   - Em caso de empate, prefira (nesta ordem):
     1. Soma mais baixa
     2. Valores selecionados em ordem lexicograficamente menor
   - Prepare um objeto `SolverResult` com os dados relevantes.

4. **Gerar uma saída legível**
   - Utilize `ReportFormatter` (ajustando se necessário) para exibir cada cenário.
   - A saída deve ser fácil de ler e comparar.

5. **Documentar decisões**
   - Adicione um `SOLUTIONS.md` resumindo(a lista abaixo são sugestões, sinta-se livre para escolher o que for mais relevante):
     - Como você organizou o parsing.
     - Como implementou o algoritmo (complexidade, trade-offs).
     - Testes/checagens manuais executados.
     - Qualquer suposição ou decisão técnica relevante.
     - Qual foi sua linha de raciocínio ao longo do processo.

6. **Commit**
   - Crie uma branch `solution/{seu_nome}`.
   - Faça commits frequentes com mensagens claras.
## Regras importantes

- **Não** remova os cenários nem simplifique o problema.
- **Não** crie testes automáticos adicionais; descreva testes no `SOLUTIONS.md`.
- Você pode reorganizar o código, criar arquivos novos, ajustar namespaces, adicionar pacotes NuGet ou alterar a saída desde que explique suas escolhas.
- Mantenha o idioma original do código (inglês) e utilize comentários apenas quando realmente agregarem clareza.

**Boa sorte, e lembre-se: o foco é avaliar a forma como você pensa para chegar ao resultado final. Caso não consiga fazer a aplicação rodar em erros, não tem problema, o que nós importa é o 
`SULUTIONS.md` criado por você.**
