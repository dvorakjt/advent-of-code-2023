class Maze
{
  private Tile[,] Tiles;
  private (int Row, int Column) StartingPoint;
  private List<MazeRunner> mazeRunners = new();

  public Maze(Tile[,] tiles, (int Row, int Column) startingPoint)
  {
    Tiles = tiles;
    StartingPoint = startingPoint;
  }

  public long FindFurthestPoint()
  {
    MazeRunner north = new MazeRunner(StartingPoint.Row, StartingPoint.Column, Direction.N);
    MazeRunner south = new MazeRunner(StartingPoint.Row, StartingPoint.Column, Direction.S);
    MazeRunner east = new MazeRunner(StartingPoint.Row, StartingPoint.Column, Direction.E);
    MazeRunner west = new MazeRunner(StartingPoint.Row, StartingPoint.Column, Direction.W);

    mazeRunners = new() { north, south, east, west };

    while(true)
    {
      foreach(var mazeRunner in mazeRunners)
      {
        if(!mazeRunner.ReachedDeadEnd)
        {
          bool loopFound = mazeRunner.MoveAndCheckIfLoopFound(this);
          if(loopFound)
          {
            return mazeRunner.StepsTaken;
          }
        }
      }
    }
  }

  private class MazeRunner
  {
    public Direction NextDirection;
    public long StepsTaken { get; private set; } = 0;
    public int Row { get; private set; }
    public int Column { get; private set; }

    public bool ReachedDeadEnd { get; private set; } = false;

    public MazeRunner(int row, int column, Direction nextDirection)
    {
      Row = row;
      Column = column;
      NextDirection = nextDirection;
    }

    public bool MoveAndCheckIfLoopFound(Maze maze)
    {
      if (
        (NextDirection == Direction.N && Row == 0) ||
        (NextDirection == Direction.W && Column == 0) ||
        (NextDirection == Direction.S && Row == maze.Tiles.GetLength(0) - 1) ||
        (NextDirection == Direction.E && Column == maze.Tiles.GetLength(1) - 1)
      )
      {
        ReachedDeadEnd = true;
        return false;
      }

      Tile nextTile;
      Direction? nextNextDirection;

      switch(NextDirection)
      {
        case Direction.N:
          Row--;
          break;
        case Direction.W:
          Column--;
          break;
        case Direction.S:
          Row++;
          break;
        case Direction.E:
          Column++;
          break;
      }

      nextTile = maze.Tiles[Row, Column];
      ReachedDeadEnd = !nextTile.TryMoveTo(NextDirection, out nextNextDirection);

      if(nextNextDirection != null) NextDirection = (Direction)nextNextDirection;

      
      if(!ReachedDeadEnd)
      {
        StepsTaken++;
        return maze.mazeRunners.Any(mazeRunner => {
          bool arrivedAtOtherRunner = 
            mazeRunner != this && 
            !mazeRunner.ReachedDeadEnd &&
            mazeRunner.Row == Row && 
            mazeRunner.Column == Column;
          return arrivedAtOtherRunner;
        });
      }

      return false;
    }
  }
}