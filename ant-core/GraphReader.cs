using System;
using System.IO;
using System.Linq;

namespace Aco
{
    public class GraphReader
    {
        private enum Symbol
        {
            COMMENT_SYMBOL = 'c',
            VERTEX_COUNT_SYMBOL = 'p',
            VERTEX_SYMBOL = 'i'
        }

        public static Graph ReadFromPath(string path)
        {
            double[][] matrix = null;
            bool isVertexCountParsered = false;
            int redVertexCount = 0;
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                switch (line[0])
                {
                    case (char)Symbol.COMMENT_SYMBOL:
                        continue;
                    case (char)Symbol.VERTEX_COUNT_SYMBOL:
                        if (isVertexCountParsered)
                        {
                            throw new Exception(
                                "Invalid graph file: vertex count specified more than once.\n"
                            );
                        }
                        else
                        {
                            int vertexCount = int.Parse(line.Split(' ')[1]);
                            matrix = new double[vertexCount][];
                            for (var i = 0; i < vertexCount; i++)
                            {
                                matrix[i] = new double[vertexCount];
                            }
                            isVertexCountParsered = true;
                        }
                        break;
                    case (char)Symbol.VERTEX_SYMBOL:
                        if (!isVertexCountParsered)
                        {
                            throw new Exception(
                                "Invalid graph file: vertex comes before vertex count definition.\n"
                            );
                        }
                        else if (redVertexCount >= matrix.Length)
                        {
                            throw new Exception(
                                "\"Invalid graph file: mismatch between defined vertex count and actual vertex count.\n"
                            );
                        }
                        else
                        {
                            double[] currentVertex = matrix[redVertexCount++];
                            Array.Copy(
                                line.Trim()
                                    .Split(' ')
                                    .Skip(1)
                                    .Select(x => double.Parse(x))
                                    .ToArray(),
                                currentVertex,
                                currentVertex.Length
                            );
                        }
                        break;
                    default:
                        throw new Exception(
                            "Invalid graph file: unknown line identifier.\n"
                        );
                }
            }

            if (redVertexCount != matrix.Length)
            {
                throw new Exception(
                    "\"Invalid graph file: mismatch between defined vertex count and actual vertex count.\n"
                );
            }

            return new Graph(matrix);
        }
    }
}
