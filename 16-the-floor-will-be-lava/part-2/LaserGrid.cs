class LaserGrid
{
  private Tile[,] Tiles;
  private List<Beam> Beams = new();

  public LaserGrid(char[,] tiles)
  {
    Tiles = new Tile[tiles.GetLength(0), tiles.GetLength(1)];

    for(int i = 0; i < tiles.GetLength(0); i++)
    {
      for(int j = 0; j < tiles.GetLength(1); j++)
      {
        Tiles[i,j] = new Tile(tiles[i,j]);
      }
    }
  }

  private int CountEnergizedTiles(Point startingPoint, Direction startingDirection) 
  {
    int energizedTiles = 0;
    Beam beam = new Beam(startingPoint, startingDirection);
    Beams.Add(beam);

    while(Beams.Count > 0)
    {
      List<Beam> updatedBeams = new();

      foreach(Beam b in Beams)
      {
        b.Move();

        //delete the beam if it has left the grid
        if(
          b.Position.Row < 0 ||
          b.Position.Row >= Tiles.GetLength(0) ||
          b.Position.Column < 0 ||
          b.Position.Column >= Tiles.GetLength(1)
        )
        {
          continue;
        }

        //visit the tile
        Tile tile = Tiles[b.Position.Row, b.Position.Column];
        if(tile.EnteredFrom.Contains(b.Direction)) continue;

        if(!tile.Energized)
        {
          energizedTiles++;
        }

        tile.Visit(b.Direction);

        //if the tile contains a reflector, call the beam's reflect method
        if(tile.Content == '/' || tile.Content == '\\')
        {
          b.Reflect(tile.Content);
        }

        //if the tile contains a splitter, call the beam's split method
        if(tile.Content == '-' || tile.Content == '|')
        {
          Beam? splitBeam = b.Split(tile.Content);

          if(splitBeam != null)
          {
            updatedBeams.Add(splitBeam);
          }
        }

        //add the beam to updated beams
        updatedBeams.Add(b);
      }

      Beams = updatedBeams;
    }

    ResetTiles();

    return energizedTiles;
  }

  private void ResetTiles()
  {
    for(int i = 0; i < Tiles.GetLength(0); i++)
    {
      for(int j = 0; j < Tiles.GetLength(1); j++)
      {
        Tiles[i,j] = new Tile(Tiles[i,j].Content);
      }
    }
  }

  public int GetMaxEnergizedTiles()
  {
    int maxEnergizedTiles = 0;

    for(int row = 0; row < Tiles.GetLength(0); row++)
    {
      int westToEastCount = CountEnergizedTiles(new Point(row, -1), Direction.EAST);
      if(westToEastCount > maxEnergizedTiles) maxEnergizedTiles = westToEastCount;

      int eastToWestCount = CountEnergizedTiles(new Point(row, Tiles.GetLength(1)), Direction.WEST);
      if(eastToWestCount > maxEnergizedTiles) maxEnergizedTiles = eastToWestCount;
    }

    for(int column = 0; column < Tiles.GetLength(1); column++)
    {
      int northToSouthCount = CountEnergizedTiles(new Point(-1, column), Direction.SOUTH);
      if(northToSouthCount > maxEnergizedTiles) maxEnergizedTiles = northToSouthCount;

      int southToNorthCount = CountEnergizedTiles(new Point(Tiles.GetLength(0), column), Direction.NORTH);
      if(southToNorthCount > maxEnergizedTiles) maxEnergizedTiles = southToNorthCount;
    }

    return maxEnergizedTiles;
  }

}