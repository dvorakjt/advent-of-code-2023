class Lavaduct
{
  List<PerimeterTile> PerimeterTiles = new();

  public void AddPerimeterTile(PerimeterTile perimeterTile)
  {
    PerimeterTiles.Add(perimeterTile);
  }

  public double LocateOuterCornersAndCalculateArea()
  {
    LocateOuterCorners();
    
    return CalculateArea();
  }

  private void LocateOuterCorners()
  {
    int indexOfTopLeftCornerTile = GetIndexOfTopLeftCornerTile();

    PerimeterTiles[indexOfTopLeftCornerTile].SetOuterCorner(Side.TOP, Side.LEFT);

    for(int i = indexOfTopLeftCornerTile + 1; i < PerimeterTiles.Count; i++)
    {
      PerimeterTiles[i].SetOuterCorner(PerimeterTiles[i - 1]);
    }

    PerimeterTile lastTile = PerimeterTiles.Last();

    if(lastTile.OuterCorner == null)
    {
      throw new InvalidOperationException("Expected SetOuterCorner() method of PerimeterTiles.Last() to have been called.");
    }

    Corner lastTileOuterCorner = (Corner)lastTile.OuterCorner;

    PerimeterTiles[0].SetOuterCorner(lastTileOuterCorner.TopOrBottom, lastTileOuterCorner.LeftOrRight);

    for(int i = 1; i < indexOfTopLeftCornerTile; i++)
    {
      PerimeterTiles[i].SetOuterCorner(PerimeterTiles[i - 1]);
    }
  }

  private int GetIndexOfTopLeftCornerTile()
  {
    int indexOfTopLeftCornerTile = -1;
    int minX = int.MaxValue;
    int minY = int.MaxValue;

    for(int i = 0; i < PerimeterTiles.Count; i++)
    { 
      var perimeterTile = PerimeterTiles[i];

      if(perimeterTile.CenterX < minX)
      {
        minX = perimeterTile.CenterX;
        minY = perimeterTile.CenterY;
        indexOfTopLeftCornerTile = i;
      }
      else if(perimeterTile.CenterX == minX && perimeterTile.CenterY < minY)
      {
        minX = perimeterTile.CenterX;
        minY = perimeterTile.CenterY;
        indexOfTopLeftCornerTile = i;
      }
    }

    return indexOfTopLeftCornerTile;
  }

  private double CalculateArea()
  {
    double totalArea = 0;

    for(int i = 1; i < PerimeterTiles.Count; i++)
    {
      PerimeterTile a = PerimeterTiles[i - 1];
      PerimeterTile b = PerimeterTiles[i];

      if
      (
        a.OuterCornerX == null || 
        b.OuterCornerX == null || 
        a.OuterCornerY == null || 
        b.OuterCornerY == null
      )
      {
        throw new ArgumentNullException("OuterCornerX and OuterCornerY properties of all perimeter tiles must not be null when CalculateArea() is called.");
      }

      double contribution = 
        ((double)a.OuterCornerX * (double)b.OuterCornerY) - 
        ((double)a.OuterCornerY * (double)b.OuterCornerX);

      totalArea += contribution;
    }

    totalArea /= 2;

    return totalArea;
  }
}