using CommandLine;

namespace AcoDemo;

internal class Options
{
    [Option('r',
           "random-numbers-source-path",
           Required = true,
           HelpText = "Path to file to serve as random numbers source.")]
    public string RandomNumbersSourcePath { get; set; }

    [Option('p',
            "graph-path",
            Required = true,
            HelpText = "Path to file containing graph description.")]
    public string GraphPath { get; set; }

    [Option('f',
            "initial-ferment",
            Required = true,
            HelpText = "Initial ferment value.")]
    public double InitialFermentValue { get; set; }

    [Option('a',
            "cost-coefficient",
            Required = true,
            HelpText = "Probability cost coefficient.")]
    public double CostCoefficient {  get; set; }

    [Option('b',
            "ferment-coefficient",
            Required = true,
            HelpText = "Probability ferment coefficient.")]
    public double FermentCoefficient {  get; set; }

    [Option('s',
            "vertex-index-start",
            Required = true,
            HelpText = "Index of start vertex.")]
    public int ViStart { get; set; }

    [Option('e',
            "vertex-index-end",
            Required = true,
            HelpText = "Index of end vertex.")]
    public int ViEnd { get; set; }

    [Option('t',
            "ant-count",
            Required = true,
            HelpText = "Ant count.")]
    public int AntCount { get; set; }

    [Option('i',
            "max-iteration-count",
            Required = true,
            HelpText = "Max iteration count.")]
    public ulong MaxIterationCount { get; set; }

    [Option('q',
            "ant-ferment-count",
            Required = true,
            HelpText = "Total ant ferment count. Used to calculate ferment delta.")]
    public double Q { get; set; }

    [Option('c',
            "ferment-expiration",
            Required = true,
            HelpText = "Ferment expiration coefficient. New ferment = c * old ferment + total delta.")]
    public double C { get; set; }
}
