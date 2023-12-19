class LaserGrid
{
  private Tile[,] Tiles;
  private List<Beam> Beams = new();

  public int EnergizedTiles {get; private set; } = 0; 

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

    EnergizeTiles();
  }

  private void EnergizeTiles() 
  {
    Beam beam = new Beam(new Point(0, -1), Direction.EAST);
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
          EnergizedTiles++;
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
  }
}