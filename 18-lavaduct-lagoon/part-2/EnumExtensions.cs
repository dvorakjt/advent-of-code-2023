public static class EnumExtensions
{
  public static Side Opposite(this Side side)
  {
    return side switch
    {
      Side.TOP => Side.BOTTOM,
      Side.BOTTOM => Side.TOP,
      Side.LEFT => Side.RIGHT,
      Side.RIGHT => Side.LEFT,
      _ => throw new ArgumentException("Side must be one of TOP, LEFT, BOTTOM or RIGHT."),
    };
  }
  
  public static Direction Opposite(this Direction direction)
  {
    return direction switch
    {
      Direction.UP => Direction.DOWN,
      Direction.DOWN => Direction.UP,
      Direction.LEFT => Direction.RIGHT,
      Direction.RIGHT => Direction.LEFT,
      _ => throw new ArgumentException("Direction must be one of UP, LEFT, DOWN or RIGHT."),
    };
  }
}