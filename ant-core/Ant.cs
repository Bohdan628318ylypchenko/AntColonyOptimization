using System;
using System.Collections.Generic;
using System.Linq;

namespace Aco;

public class AntCommonData
{
    public readonly Graph pathGraph;
    public readonly Graph fermentGraph;

    public readonly double costCoefficient;
    public readonly double fermentCoefficient;

    public readonly int viStart;
    public readonly int viEnd;

    public AntCommonData(
        Graph pathGraph,
        Graph fermentGraph,
        double costCoefficient,
        double fermentCoefficient,
        int viStart,
        int viEnd
    ) {
        this.pathGraph = pathGraph;
        this.fermentGraph = fermentGraph;
        this.costCoefficient = costCoefficient;
        this.fermentCoefficient = fermentCoefficient;
        this.viStart = viStart;
        this.viEnd = viEnd;
    }
}

public class Ant
{
    private readonly AntCommonData acd;

    private readonly FileRandomNumbersGenerator frndg;

    private int viCurrent;
    private readonly Path path;
    HashSet<int> availableVertexes;

    public Ant(
        AntCommonData acd, FileRandomNumbersGenerator frndg
    ) {
        this.acd = acd;
        this.frndg = frndg;

        path = new Path();

        InitializeAntState();
    }

    private void InitializeAntState()
    {
        viCurrent = acd.viStart;
        path.Append(viCurrent);

        availableVertexes =
            new HashSet<int>(
                Enumerable.Range(0, acd.pathGraph.VertexCount)
            );
        availableVertexes.Remove(viCurrent);
    }

    public void reset()
    {
        path.Clear();
        availableVertexes.Clear();
        InitializeAntState();
    }

    public Path FindPath()
    {
        while (viCurrent != acd.viEnd && availableVertexes.Count != 0)
        {
            double[] currentVertexPathWeights = acd.pathGraph[viCurrent];
            double[] currentVertexFermentWeights = acd.fermentGraph[viCurrent];

            Dictionary<int, double> vip = new Dictionary<int, double>();
            double psum = 0;
            foreach (var vi in availableVertexes)
            {
                double p =
                    Math.Pow((1.0 / currentVertexPathWeights[vi]), acd.costCoefficient) *
                    Math.Pow(currentVertexFermentWeights[vi], acd.fermentCoefficient);

                vip.Add(vi, p);
                psum += p;
            }

            //vip = vip.Select((k, v) => (k, v / psum)).ToDictionary();
            foreach (var vi in vip.Keys)
            {
                vip[vi] /= psum;
            }

            double trial = frndg.NextUniformDouble();
            double tsum = 0;
            foreach (var (vi, p) in vip)
            {
                tsum += p;
                if (trial < tsum)
                {
                    path.Append(vi, currentVertexPathWeights[vi]);
                    availableVertexes.Remove(vi);
                    viCurrent = vi;
                    break;
                }
            }
        }

        return path;
    }
}
