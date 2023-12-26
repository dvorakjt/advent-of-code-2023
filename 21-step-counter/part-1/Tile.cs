

class Tile(int row, int column, bool canBeVisited, int minSteps) : IComparable<Tile>
{
  public readonly int Row = row;
  public readonly int Column = column;
  public bool CanBeVisited = canBeVisited;
  public int MinSteps = minSteps;

  public int CompareTo(Tile? other)
  {
    if(other == null) return -1;

    return MinSteps - other.MinSteps;
  }
}