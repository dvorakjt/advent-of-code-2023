class PerimeterTile 
{
  private Direction DirectionToPrevious;
  private Direction DirectionToCurrent;
  private Direction DirectionToNext;
  public int CenterX { get; private set; }
  public int CenterY { get; private set; }
  public Corner? OuterCorner { get; private set; }
  public double? OuterCornerX { get; private set; }
  public double? OuterCornerY { get; private set; }

  public PerimeterTile(
    int centerX, 
    int centerY,
    Direction directionToPrevious,
    Direction directionToCurrent,
    Direction directionToNext
  )
  {
    CenterX = centerX;
    CenterY = centerY;
    DirectionToPrevious = directionToPrevious;
    DirectionToCurrent = directionToCurrent;
    DirectionToNext = directionToNext;
  }

  public PerimeterTile(PerimeterTile perimeterTile)
  {
    CenterX = perimeterTile.CenterX;
    CenterY = perimeterTile.CenterY;
    DirectionToPrevious = perimeterTile.DirectionToPrevious;
    DirectionToCurrent = perimeterTile.DirectionToCurrent;
    DirectionToNext = perimeterTile.DirectionToNext;
  }

  public void SetOuterCorner(Side topOrBottom, Side leftOrRight)
  { 
    OuterCorner = new Corner(topOrBottom, leftOrRight);
    SetOuterCornerX();
    SetOuterCornerY();
  }

  public void SetOuterCorner
  (
    PerimeterTile previousTile
  )
  {
    if(previousTile.OuterCorner == null)
    {
      throw new ArgumentNullException("The previous tile's OuterCorner must have been set in order to determine this tile's outer corner.");
    }

    Corner previousOuterCorner = (Corner)previousTile.OuterCorner;
    Side topOrBottom = GetTopOrBottom(previousOuterCorner);
    Side leftOrRight = GetLeftOrRight(previousOuterCorner);
  
    OuterCorner = new Corner(topOrBottom, leftOrRight);

    SetOuterCornerX();
    SetOuterCornerY();
  }

  private Side GetTopOrBottom
  (
    Corner previousOuterCorner
  )
  {
    if
    (
      (DirectionToCurrent == Direction.UP || DirectionToCurrent == Direction.DOWN) 
      && 
      DirectionToPrevious == DirectionToNext.Opposite()
    )
    {
      return previousOuterCorner.TopOrBottom.Opposite();
    }

    return previousOuterCorner.TopOrBottom;
  }

  private Side GetLeftOrRight
  (
    Corner previousOuterCorner
  )
  {
    if
    (
      (DirectionToCurrent == Direction.LEFT || DirectionToCurrent == Direction.RIGHT) 
      && 
      DirectionToPrevious == DirectionToNext.Opposite()
    )
    {
      return previousOuterCorner.LeftOrRight.Opposite();
    }

    return previousOuterCorner.LeftOrRight;
  }

  private void SetOuterCornerX()
  {
    if(OuterCorner == null) throw new InvalidOperationException("Cannot set OuterCornerX when OuterCorner is null.");
    
    Corner outerCorner = (Corner)OuterCorner;

    if(outerCorner.LeftOrRight == Side.LEFT)
    {
      OuterCornerX = CenterX - 0.5;
    }
    else
    {
      OuterCornerX = CenterX + 0.5;
    }
  }

  private void SetOuterCornerY()
  {
    if(OuterCorner == null) throw new InvalidOperationException("Cannot set OuterCornerY when OuterCorner is null.");

    Corner outerCorner = (Corner)OuterCorner;

    if(outerCorner.TopOrBottom == Side.TOP)
    {
      OuterCornerY = CenterY - 0.5;
    }
    else
    {
      OuterCornerY = CenterY + 0.5;
    }
  }
}