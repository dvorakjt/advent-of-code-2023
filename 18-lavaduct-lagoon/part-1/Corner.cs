struct Corner
{
  public Side TopOrBottom { get; private set; }
  public Side LeftOrRight { get; private set; }

  public Corner(Side topOrBottom, Side leftOrRight)
  {
    if
    (
      topOrBottom == Side.LEFT ||
      topOrBottom == Side.RIGHT ||
      leftOrRight == Side.TOP ||
      leftOrRight == Side.BOTTOM
    )
    {
      throw new ArgumentException("topOrBottom must be Side.TOP or Side.BOTTOM and leftOrRight must be Side.LEFT or Side.RIGHT");
    }

    TopOrBottom = topOrBottom;
    LeftOrRight = leftOrRight;
  }
}