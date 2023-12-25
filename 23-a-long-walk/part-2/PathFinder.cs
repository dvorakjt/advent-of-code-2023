class PathFinder
{
  private readonly bool[,] ForestMap;
  private readonly Point Origin = new(0, 1);
  private readonly Point Destination;
  private readonly Dictionary<Point, List<Neighbor>> JunctionAdjacencyLists = [];
  public int LongestPathThroughJunctions { get; private set; }

  public PathFinder(bool [,] forestMap)
  {
    ForestMap = forestMap;
    Destination = new Point(ForestMap.GetLength(0) - 1, ForestMap.GetLength(1) - 2);
    JunctionAdjacencyLists.Add(Origin, []);
    JunctionAdjacencyLists.Add(Destination, []);

    FindJunctions();

    Console.WriteLine("Found junctions. Finding longest path...");

    FindLongestPathThroughJunctions();
  }

  private void FindJunctions()
  {
    FindJunctions(Origin);
  }
  private void FindJunctions(Point junction)
  {
    foreach(var directionToNeighbor in Enum.GetValues<Direction>())
    {
      if
      (
        DescribedNeighbor(junction, directionToNeighbor) ||
        !CanMoveInDirection(junction, directionToNeighbor)
      )
      {
        continue;
      }

      Point next = Move(junction, directionToNeighbor);
      HashSet<Point> history = [junction, next];
      Direction direction = directionToNeighbor;
      int distance = 1;
      bool foundJunction = IsJunction(next); 

      while(!foundJunction)
      {
        bool foundDirection = false;

        foreach(var d in Enum.GetValues<Direction>())
        {
          if(CanMoveInDirection(next, d) && !history.Contains(Move(next, d)))
          {
            direction = d;
            next = Move(next, direction);
            history.Add(next);
            distance++;
            foundJunction = IsJunction(next);
            foundDirection = true;
            break;
          }
        }

        if(!foundDirection)
        {
          throw new InvalidOperationException($"Could not find direction to move. Origin: {junction}, currentPoint: {next}");
        }
      }

      JunctionAdjacencyLists[junction].Add(new Neighbor(next, directionToNeighbor, distance));
      
      if(!JunctionAdjacencyLists.ContainsKey(next))
      {
        JunctionAdjacencyLists.Add(next, []);
      }

      JunctionAdjacencyLists[next].Add(new Neighbor(junction, direction.Opposite(), distance));

      FindJunctions(next);
    }
  }

  private bool DescribedNeighbor(Point junction, Direction directionToNeighbor)
  {
    return JunctionAdjacencyLists[junction].Any(neighbor => neighbor.DirectionFromOther == directionToNeighbor);
  }

  private bool IsJunction(Point point)
  {
    if(JunctionAdjacencyLists.ContainsKey(point)) return true;

    int possibleDirectionsOfMovement = 0;

    foreach(var direction in Enum.GetValues<Direction>())
    {
      if(CanMoveInDirection(origin: point, direction))
      {
        possibleDirectionsOfMovement++;
      }
    }

    return possibleDirectionsOfMovement > 2;
  }

  private bool CanMoveInDirection(Point origin, Direction direction)
  {
    Point destination = Move(origin, direction);
    
    return 
      destination.Row >= 0 && 
      destination.Row < ForestMap.GetLength(0) && 
      destination.Column >= 0 && 
      destination.Column < ForestMap.GetLength(1) && 
      ForestMap[destination.Row, destination.Column];
  }

  private static Point Move(Point origin, Direction direction)
  {
    int row = 
      direction == Direction.NORTH ? origin.Row - 1 : (direction == Direction.SOUTH ? origin.Row + 1 : origin.Row);
    int column = 
      direction == Direction.WEST ? origin.Column - 1 : (direction == Direction.EAST ? origin.Column + 1 : origin.Column);

    return new Point(row, column);
  }

  private void FindLongestPathThroughJunctions()
  {
    FindLongestPathThroughJunctions
      (
        junctionPoint: Origin,
        length: 0, 
        history: [Origin]
      );
  }

  private void FindLongestPathThroughJunctions(Point junctionPoint, int length, HashSet<Point> history)
  {
    if(junctionPoint == Destination && length > LongestPathThroughJunctions)
    {
      LongestPathThroughJunctions = length;
    }

    List<Neighbor> neighboringJunctions = JunctionAdjacencyLists[junctionPoint];

    List<Neighbor> unvisitedJunctions = neighboringJunctions.Where(j => !history.Contains(j.Point)).ToList();

    foreach(var unvisitedJunction in unvisitedJunctions)
    {
      HashSet<Point> updatedHistory = new(history)
      {
        unvisitedJunction.Point
      };
      int updatedLength = length + unvisitedJunction.DistanceFromOther;

      FindLongestPathThroughJunctions(unvisitedJunction.Point, updatedLength, updatedHistory);
    }
  }
}