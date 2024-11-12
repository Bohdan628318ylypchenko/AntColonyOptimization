using System.Linq;

namespace Aco;

public class Graph
{
    private static readonly double FULL_ADJUSTMENT_COEFFICIENT = 100;

    private double[][] matrix;

    public int VertexCount => matrix.Length;

    public double[] this[int i] => matrix[i];

    public double this[int i, int j] => matrix[i][j];

    public Graph(double[][] matrix)
    {
        this.matrix = matrix;
        AdjustToFull();
    }

    public Graph(double weight, int vertexCount)
    {
        matrix = new double[vertexCount][];
        for (var i = 0; i < vertexCount; i++)
        {
            matrix[i] = new double[vertexCount];
        }

        FillAllWithWeight(weight);
    }

    public void FillAllWithWeight(double weight)
    {
        for (var i = 0; i < VertexCount; i++)
        {
            for (var j = 0; j < VertexCount; j++)
            {
                matrix[i][j] = weight;
            }
        }
    }

    private void AdjustToFull()
    {
        double replacement =
            matrix.Select(x => x.Max()).Max() *
            VertexCount *
            FULL_ADJUSTMENT_COEFFICIENT;
        for (var i = 0; i < VertexCount; i++)
        {
            for (var j = 0; j < VertexCount; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][j] = replacement;
                }
            }
        }
    }
}
