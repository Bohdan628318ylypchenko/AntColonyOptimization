using Aco;

namespace AcoTest;

internal class FileRandomNumbersGeneratorTests
{
    private static readonly string RANDOM_NUMBERS_SOURCE_FILENAME =
        "normalized-quantum-big.rnd";

    [Test]
    public void InitializeCreateRead()
    {
        FileRandomNumbersGenerator.InitializeRandomNumbersSource(
            RANDOM_NUMBERS_SOURCE_FILENAME
        );

        var frndg = new FileRandomNumbersGenerator(0);
        for (var i = 0; i < FileRandomNumbersGenerator.GetRandomNumbersCount() * 2; i++)
        {
            Assert.That(frndg.NextUniformDouble(), Is.GreaterThanOrEqualTo(0));
        }
    }
}