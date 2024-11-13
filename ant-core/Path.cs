using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aco;

public class Path
{
    private readonly List<int> trajectory;
    private double totalCost;

    public IReadOnlyList<int> Trajectory => trajectory;
    public double TotalCost => totalCost;

    public Path()
    {
        trajectory = new List<int>();
        totalCost = 0;
    }

    public Path(double initialCost)
    {
        totalCost = initialCost;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (!(obj is Path)) return false;

        if (obj == this) return true;

        Path other = (Path)obj;

        return totalCost == other.totalCost &&
               trajectory.SequenceEqual(other.trajectory);
    }

    public override int GetHashCode()
    {
        return totalCost.GetHashCode() + trajectory.GetHashCode();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"TotalCost = {totalCost}");
        sb.AppendLine($"Trajectory = [{string.Join(",", trajectory.Select(x => x.ToString()))}]");
        return sb.ToString();
    }

    public void Append(int vi)
    {
        trajectory.Add(vi);
    }

    public void Append(int vi, double cost)
    {
        trajectory.Add(vi);
        totalCost += cost;
    }

    public void Clear()
    {
        trajectory.Clear();
        totalCost = 0;
    }
}
