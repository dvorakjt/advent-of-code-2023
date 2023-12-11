class Maze
{
  private Tile[,] Tiles;
  private Point StartingPoint;
  private List<MazeRunner> MazeRunners = new();
  public int Height 
  {
    get 
    {
      return Tiles.GetLength(0);
    }
  }

  public int Width {
    get 
    {
      return Tiles.GetLength(1);
    }
  }


  public Maze(Tile[,] tiles, Point startingPoint)
  {
    Tiles = tiles;
    StartingPoint = startingPoint;
  }

  public List<Point> FindLoopPath()
  {
    List<Point> loopPath = new()
    {
      StartingPoint
    };

    MazeRunner north = new MazeRunner(StartingPoint, Direction.N);
    MazeRunner south = new MazeRunner(StartingPoint, Direction.S);
    MazeRunner east = new MazeRunner(StartingPoint, Direction.E);
    MazeRunner west = new MazeRunner(StartingPoint, Direction.W);

    MazeRunners = new() { north, south, east, west };

    while(true)
    {
      foreach(var mazeRunner in MazeRunners)
      {
        if(!mazeRunner.ReachedDeadEnd)
        {
          bool loopFound = mazeRunner.MoveAndCheckIfLoopFound(this);
          if(loopFound)
          {
            loopPath.AddRange(mazeRunner.Path);
            return loopPath;
          }
        }
      }
    }
  }

  private class MazeRunner
  {
    public Direction NextDirection;
    public Point Location;
    public List<Point> Path = new();

    public bool ReachedDeadEnd { get; private set; } = false;

    public MazeRunner(Point startingLocation, Direction nextDirection)
    {
      Location = startingLocation;
      NextDirection = nextDirection;
    }

    public bool MoveAndCheckIfLoopFound(Maze maze)
    {
      if (
        (NextDirection == Direction.N && Location.Row == 0) ||
        (NextDirection == Direction.W && Location.Column == 0) ||
        (NextDirection == Direction.S && Location.Row == maze.Tiles.GetLength(0) - 1) ||
        (NextDirection == Direction.E && Location.Column == maze.Tiles.GetLength(1) - 1)
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
          Location = new Point(Location.Row - 1, Location.Column);
          break;
        case Direction.W:
          Location = new Point(Location.Row, Location.Column - 1);
          break;
        case Direction.S:
          Location = new Point(Location.Row + 1, Location.Column);
          break;
        case Direction.E:
          Location = new Point(Location.Row, Location.Column + 1);
          break;
      }

      nextTile = maze.Tiles[Location.Row, Location.Column];
      ReachedDeadEnd = !nextTile.TryMoveTo(NextDirection, out nextNextDirection);

      if(nextNextDirection != null) NextDirection = (Direction)nextNextDirection;

      
      if(!ReachedDeadEnd)
      {
        bool arrivedAtOtherRunner = false;

        foreach(var mazeRunner in maze.MazeRunners)
        {
          arrivedAtOtherRunner = mazeRunner != this && 
            !mazeRunner.ReachedDeadEnd &&
            mazeRunner.Location == Location;

          if(arrivedAtOtherRunner)
          {
            mazeRunner.Path.Reverse();
            Path.AddRange(mazeRunner.Path);
            return true;
          }
        }

        if(!arrivedAtOtherRunner)
        {
          Path.Add(Location);
        }
      }

      return false;
    }
  }
}