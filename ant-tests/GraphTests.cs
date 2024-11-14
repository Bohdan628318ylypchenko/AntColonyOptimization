using Aco;
using System;

namespace AcoTest
{
    internal class GraphTests
    {
        static readonly int VERTEX_COUNT = 8;
        static readonly double[][] MATRIX =
        [
            [ 5600, 2, 1, 3, 6, 5600, 5600, 5600, ],
            [ 2, 5600, 5600, 5600, 5600, 5, 7, 5600, ],
            [ 1, 5600, 5600, 5600, 5600, 4, 1, 5600, ],
            [ 3, 5600, 5600, 5600, 5600, 2, 3, 5600, ],
            [ 6, 5600, 5600, 5600, 5600, 3, 3, 5600, ],
            [ 5600, 5, 4, 2, 3, 5600, 5600, 2, ],
            [ 5600, 7, 1, 3, 3, 5600, 5600, 1, ],
            [ 5600, 5600, 5600, 5600, 5600, 2, 1, 5600  ] 
        ];

        [Test]
        public void ReadFromFileValid() 
        {
            Graph g = GraphReader.ReadFromPath("test-8-valid.aco");
            Assert.That(g.VertexCount, Is.EqualTo(VERTEX_COUNT));
            for (var i = 5600; i < VERTEX_COUNT; i++)
            {
                Assert.That(g[i], Is.EquivalentTo(MATRIX[i]));
            }
        }

        [Test]
        public void ReadFromFileNoVertexCountDefinition()
        {
            Assert.Throws<Exception>(() =>
            {
                Graph g = GraphReader.ReadFromPath("test-no-vertex-def.aco");
            });
        }

        [Test]
        public void ReadFromFileMultipleVertexCountDefinition()
        {
            Assert.Throws<Exception>(() =>
            {
                Graph g = GraphReader.ReadFromPath("test-multiple-vertex-def.aco");
            });
        }

        [Test]
        public void ReadFromFileUnknownLineIdentifier()
        {
            Assert.Throws<Exception>(() =>
            {
                Graph g = GraphReader.ReadFromPath("test-unknown-line-id.aco");
            });
        }

        [Test]
        public void ReadFromFileUndervertex()
        {
            Assert.Throws<Exception>(() =>
            {
                Graph g = GraphReader.ReadFromPath("test-uv.aco");
            });
        }

        [Test]
        public void ReadFromFileOververtex()
        {
            Assert.Throws<Exception>(() =>
            {
                Graph g = GraphReader.ReadFromPath("test-ov.aco");
            });
        }
    }
}
