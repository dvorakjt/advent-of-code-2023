using System.Diagnostics.CodeAnalysis;

struct Point {
  public int Row { get; private set; }
  public int Column { get; private set; }

  public Point(int row, int column)
  {
    Row = row;
    Column = column;
  }

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if(obj == null || obj.GetType() != this.GetType())
    {
      return false;
    }

    Point other = (Point)obj;

    return other.Row == Row && other.Column == Column;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Row, Column);
  }

  public static bool operator == (Point p1, Point p2) 
  {
      return p1.Equals(p2);
  }

  public static bool operator != (Point p1, Point p2) 
  {
    return !p1.Equals(p2);
  }
}