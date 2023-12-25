public static class EnumExtensions 
{
  public static Direction Opposite(this Direction direction)
  {
    return direction switch
    {
      Direction.NORTH => Direction.SOUTH,
      Direction.SOUTH => Direction.NORTH,
      Direction.WEST => Direction.EAST,
      Direction.EAST => Direction.WEST,
      _ => throw new ArgumentException("Direction must be one of NORTH, SOUTH, EAST, WEST."),
    };
  }
}