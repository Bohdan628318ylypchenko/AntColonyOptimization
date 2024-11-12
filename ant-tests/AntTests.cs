using Aco;
using System.Collections.Generic;

namespace AcoTest;

internal class AntTests
{
    private static readonly string RANDOM_NUMBERS_SOURCE_FILENAME =
        "normalized-quantum-big.rnd";

    [Test]
    public void FindPath()
    {
        FileRandomNumbersGenerator.InitializeRandomNumbersSource(
            RANDOM_NUMBERS_SOURCE_FILENAME
        );

        Graph g = GraphReader.ReadFromPath("test-valid.aco");
        Graph f = new Graph(0.0, g.VertexCount);

        AntCommonData acd = new AntCommonData(
            g, f, 1, 0, 0, 7
        );

        var frndg = new FileRandomNumbersGenerator(29754);

        Ant ant = new Ant(acd, frndg);

        Path p = ant.FindPath();

        Assert.That(
            p.Trajectory,
            Is.EquivalentTo(new List<int> { 0, 2, 6, 7 })
        );
        Assert.That(p.TotalCost, Is.EqualTo(3.0));
    }
}
