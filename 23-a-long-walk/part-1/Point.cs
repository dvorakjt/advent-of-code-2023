using System.Diagnostics.CodeAnalysis;

readonly struct Point(int row, int column)
{
  public readonly int Row = row;
  public readonly int Column = column;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    return obj is Point other && Equals(other);
  }

  public bool Equals(Point other) 
  {
    return Row == other.Row && Column == other.Column;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Row, Column);
  }

  public static bool operator == (Point a, Point b)
  {
    return a.Equals(b);
  }

  public static bool operator != (Point a, Point b)
  {
    return !(a == b);
  }
}