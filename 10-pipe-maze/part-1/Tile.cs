class Tile
{
  public readonly Direction[]? EntranceDirections;
  public readonly Direction[]? ExitDirections;

  public Tile(char shape)
  { 
    switch(shape)
    {
      case '|' :
        EntranceDirections = new Direction[] { Direction.N, Direction.S };
        ExitDirections = new Direction[] { Direction.N, Direction.S };
        break;
      case '-' :
        EntranceDirections = new Direction[] { Direction.E, Direction.W };
        ExitDirections = new Direction[] { Direction.E, Direction.W };
        break;
      case 'L' :
        EntranceDirections = new Direction[] { Direction.W, Direction.S };
        ExitDirections = new Direction[] { Direction.N, Direction.E };
        break;
      case 'J' :
        EntranceDirections = new Direction[] { Direction.E, Direction.S };
        ExitDirections = new Direction[] { Direction.N, Direction.W };
        break;
      case '7' :
        EntranceDirections = new Direction[] { Direction.E, Direction.N };
        ExitDirections = new Direction[] { Direction.S, Direction.W };
        break;
      case 'F' :
        EntranceDirections = new Direction[] { Direction.W, Direction.N };
        ExitDirections = new Direction[] { Direction.S, Direction.E };
        break;
    }
  }

  public bool TryMoveTo(Direction origin, out Direction? nextDirection)
  {
    if(EntranceDirections != null && ExitDirections != null)
    {
      if(origin == EntranceDirections[0])
      {
        nextDirection = ExitDirections[0];
        return true;
      }
      else if(origin == EntranceDirections[1])
      {
        nextDirection = ExitDirections[1];
        return true;
      }
    }

    nextDirection = null;
    return false;
  }
}