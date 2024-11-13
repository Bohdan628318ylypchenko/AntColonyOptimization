using System;
using System.Diagnostics;
using Aco;
using CommandLine;

namespace AcoDemo;

internal class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options => Run(options));
    }

    private static void Run(Options options)
    {
        FileRandomNumbersGenerator.InitializeRandomNumbersSource(options.RandomNumbersSourcePath);

        Graph graph = GraphReader.ReadFromPath(options.GraphPath);

        AntColony ac = new AntColony(
            graph,
            options.InitialFermentValue,
            options.CostCoefficient,
            options.FermentCoefficient,
            options.ViStart,
            options.ViEnd,
            options.AntCount,
            options.MaxIterationCount,
            options.Q, options.C
        );

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        Path result = ac.FindPath();
        stopwatch.Stop();

        Console.Write($"""
        Time = {stopwatch.Elapsed}
        Result path:
        """);
        Console.Write('\n');
        Console.WriteLine(result.ToString());
    }
}
