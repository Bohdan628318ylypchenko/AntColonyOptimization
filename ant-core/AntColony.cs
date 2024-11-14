using System.Linq;

namespace Aco;

public class AntColony
{
    private readonly AntCommonData acd;

    private readonly Graph fermentDeltaGraph;
    private readonly Ant[] ants;
    private readonly ulong maxIterationCount;
    private readonly double q;
    private readonly double c;

    private Path globalMin;

    private int AntCount => ants.Length;
    private int VertexCount => acd.pathGraph.VertexCount;

    public AntColony(
        Graph pathGraph,
        double initialFerment,
        double costCoefficient,
        double fermentCoefficient,
        int viStart, int viEnd,
        int antCount,
        ulong maxIterationCount,
        double q, double c
    ) {
        acd = new AntCommonData(
            pathGraph,
            new Graph(initialFerment, pathGraph.VertexCount),
            costCoefficient, fermentCoefficient,
            viStart, viEnd
        );

        fermentDeltaGraph = new Graph(0, VertexCount);
        ants = new Ant[antCount];
        int shift = FileRandomNumbersGenerator.GetRandomNumbersCount() / antCount;
        for (var i = 0; i < antCount; i++)
        {
            ants[i] = new Ant(acd, new FileRandomNumbersGenerator(i * shift));
        }
        this.maxIterationCount = maxIterationCount;
        this.q = q;
        this.c = c;

        globalMin = new Path(double.MaxValue);
    }

    public Path FindPath()
    {
        for (ulong iterationCount = 0;
             iterationCount < maxIterationCount;
             iterationCount++)
        {
            Path[] paths = ants
                .AsParallel()
                .Select(a => a.Reset())
                .Select(a => a.FindPath())
                .ToArray();

            fermentDeltaGraph.FillAllWithWeight(0);

            foreach (Path p in paths)
            {
                double delta = q / p.TotalCost;
                for (var i = 0; i < p.Trajectory.Count - 1; i++)
                {
                    fermentDeltaGraph[p.Trajectory[i], p.Trajectory[i + 1]] += delta;
                }
            }

            for (var i = 0; i < VertexCount; i++)
            {
                for (var j = 0; j < VertexCount; j++)
                {
                    acd.fermentGraph[i][j] = c * acd.fermentGraph[i][j] + fermentDeltaGraph[i][j];
                }
            }

            Path min = paths.MinBy(p => p.TotalCost);
            if (min.TotalCost < globalMin.TotalCost)
            {
                globalMin = new Path(min);
            }
        }

        return globalMin;
    }
}
