class Tile(TileType type)
{
  private readonly TileType Type = type;
  private bool Visited = false;

  public bool CanBeVisited(Direction directionFromOtherToThis)
  {
    if(Visited) return false;

    return Type switch
    {
      TileType.PATH => true,
      TileType.EASTWARD_SLOPE => directionFromOtherToThis == Direction.EAST,
      TileType.WESTWARD_SLOPE => directionFromOtherToThis == Direction.WEST,
      TileType.NORTHWARD_SLOPE => directionFromOtherToThis == Direction.NORTH,
      TileType.SOUTHWARD_SLOPE => directionFromOtherToThis == Direction.SOUTH,
      _ => false,
    };
  }

  public void Visit()
  {
    Visited = true;
  }

  public void Unvisit()
  {
    Visited = false;
  }
}