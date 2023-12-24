class PathFinder
{
  private Tile[,] ForestMap;
  private Point Origin = new(0, 1);
  private Point Destination;
  public int LongestPath { get; private set; } = 0;

  public PathFinder(Tile[,] forestMap)
  {
    ForestMap = forestMap;
    Destination = new(ForestMap.GetLength(0) - 1, ForestMap.GetLength(1) - 2);
    FindLongestPath(Origin, 0);
  }

  private void FindLongestPath(Point location, int currentPathLength)
  {
    if(location == Destination && currentPathLength > LongestPath)
    {
      LongestPath = currentPathLength;
    }

    Tile tile = ForestMap[location.Row, location.Column];
    tile.Visit();

    if(CanMoveInDirection(location, Direction.NORTH))
    {
      Point pointToNorth = new(location.Row - 1, location.Column);
      Tile tileToNorth = ForestMap[pointToNorth.Row, pointToNorth.Column];

      if(tileToNorth.CanBeVisited(Direction.NORTH))
      {
        FindLongestPath(pointToNorth, currentPathLength + 1);
      }
    }
    if(CanMoveInDirection(location, Direction.SOUTH))
    {
      Point pointToSouth = new(location.Row + 1, location.Column);
      Tile tileToSouth = ForestMap[pointToSouth.Row, pointToSouth.Column];

      if(tileToSouth.CanBeVisited(Direction.SOUTH))
      {
        FindLongestPath(pointToSouth, currentPathLength + 1);
      }
    }
    if(CanMoveInDirection(location, Direction.WEST))
    {
      Point pointToWest = new(location.Row, location.Column - 1);
      Tile tileToWest = ForestMap[pointToWest.Row, pointToWest.Column];

      if(tileToWest.CanBeVisited(Direction.WEST))
      {
        FindLongestPath(pointToWest, currentPathLength + 1);
      }
    }
    if(CanMoveInDirection(location, Direction.EAST))
    {
      Point pointToEast = new(location.Row, location.Column + 1);
      Tile tileToEast = ForestMap[pointToEast.Row, pointToEast.Column];

      if(tileToEast.CanBeVisited(Direction.EAST))
      {
        FindLongestPath(pointToEast, currentPathLength + 1);
      }
    }

    tile.Unvisit();
  }

  private bool CanMoveInDirection(Point location, Direction direction)
  {
    return direction switch {
      Direction.EAST => location.Column + 1 < ForestMap.GetLength(1),
      Direction.WEST => location.Column - 1 >= 0,
      Direction.NORTH => location.Row - 1 >= 0,
      Direction.SOUTH => location.Row + 1 < ForestMap.GetLength(0),
      _ => throw new ArgumentException("Direction must be one of NORTH, SOUTH, EAST or WEST.")
    };
  }
}