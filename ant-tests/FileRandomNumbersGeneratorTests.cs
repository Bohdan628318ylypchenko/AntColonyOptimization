namespace ant
{
    public class FileRandomNumbersGeneratorTests
    {
        private static readonly string RANDOM_NUMBERS_SOURCE_FILENAME =
            "normalized-quantum-big.rnd";

        [Test]
        public void Test1()
        {
            FileRandomNumbersGenerator.InitializeRandomNumbersSource(
                RANDOM_NUMBERS_SOURCE_FILENAME
            );

            var frndg = new FileRandomNumbersGenerator(0);
            for (var i = 0; i < FileRandomNumbersGenerator.GetRandomNumbersCount() * 2; i++)
            {
                Assert.GreaterOrEqual(frndg.NextUniformDouble(), 0);
            }
        }
    }
}