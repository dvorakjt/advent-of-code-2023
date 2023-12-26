

class Tile : IComparable<Tile>
{
  public readonly int Row;
  public readonly int Column;
  public bool IsReachable;
  public int ShortestPathFromStart;

  public Tile(int row, int column, bool isReachable, int shortestDistanceFromStart)
  {
    Row = row;
    Column = column;
    IsReachable = isReachable;
    ShortestPathFromStart = shortestDistanceFromStart;
  }

  public Tile(Tile tile)
  {
    Row = tile.Row;
    Column = tile.Column;
    IsReachable = tile.IsReachable;
    ShortestPathFromStart = tile.ShortestPathFromStart;
  }

  public int CompareTo(Tile? other)
  {
    if(other == null) return -1;

    return ShortestPathFromStart - other.ShortestPathFromStart;
  }
}