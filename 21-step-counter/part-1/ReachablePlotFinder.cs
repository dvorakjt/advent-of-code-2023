using System.Text;

class ReachablePlotFinder
{
  private readonly int StepsNeeded;
  private readonly Tile[,] GardenMap;

  private readonly MinHeap<Tile> UnvisitedTiles = new();

  public int ReachablePlotCount { get; private set; } = 0;

  public ReachablePlotFinder(Tile[,] gardenMap, int stepsNeeded)
  {
    GardenMap = new Tile[gardenMap.GetLength(0), gardenMap.GetLength(1)];

    for(int row = 0; row < GardenMap.GetLength(0); row++)
    {
      for(int column = 0; column < GardenMap.GetLength(1); column++)
      {
        Tile tile = new(gardenMap[row,column]);
        GardenMap[row,column] = tile;

        if(tile.IsReachable)
        {
          UnvisitedTiles.Insert(tile);
        }
      }
    }

    StepsNeeded = stepsNeeded;

    FindShortestDistancesToReachableTiles();
    CountReachablePlots();
  }

  private void FindShortestDistancesToReachableTiles()
  {
    while(UnvisitedTiles.Count > 0)
    {
      Tile tile = UnvisitedTiles.Extract();
      tile.IsReachable = false;
      
      int shortestPathToNeighbor = tile.ShortestPathFromStart + 1;
      UpdateNeighbor(tile.Row - 1, tile.Column, shortestPathToNeighbor);
      UpdateNeighbor(tile.Row + 1, tile.Column, shortestPathToNeighbor);
      UpdateNeighbor(tile.Row, tile.Column - 1, shortestPathToNeighbor);
      UpdateNeighbor(tile.Row, tile.Column + 1, shortestPathToNeighbor);
    }
  }

  private void UpdateNeighbor(int row, int column, int shortestPathFromStart)
  {
    if
    (
      row >= 0 && row < GardenMap.GetLength(0) &&
      column >= 0 && column < GardenMap.GetLength(1)
    )
    {
      Tile neighbor = GardenMap[row, column];
      if(neighbor.IsReachable)
      {
        neighbor.ShortestPathFromStart = Math.Min(shortestPathFromStart, neighbor.ShortestPathFromStart);
        UnvisitedTiles.DecreaseKey(neighbor);
      }
    }
  }

  private void CountReachablePlots()
  {
    for(int row = 0; row < GardenMap.GetLength(0); row++)
    {
      for(int column = 0; column < GardenMap.GetLength(1); column++)
      {
        if(GardenMap[row,column].ShortestPathFromStart <= StepsNeeded && GardenMap[row,column].ShortestPathFromStart % 2 == StepsNeeded % 2)
        {
          ReachablePlotCount++;
        }
      }
    }
  }

  public void SaveVisualization(string id)
  {
    string html = $"<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n\t<meta charset=\"UTF-8\">\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n\t<title>{id}</title>\n</head>\n<body>";

    int rectSide = 16;
    string svgOpeningTag = 
      $"<svg viewBox=\"0 0 {GardenMap.GetLength(1) * rectSide} {GardenMap.GetLength(0) * rectSide}\" xmlns=\"http://www.w3.org/2000/svg\">\n";

    StringBuilder svgbuilder = new(svgOpeningTag);

    for(int i = 0; i < GardenMap.GetLength(0); i++)
    {
      for(int j = 0; j < GardenMap.GetLength(1); j++)
      {
        int rectX = j * rectSide;
        int rectY = i * rectSide;

        if(GardenMap[i,j].ShortestPathFromStart == 0)
        {
          Console.WriteLine(GardenMap[i,j].Row);
          Console.WriteLine(GardenMap[i,j].Column);
        }


        if(GardenMap[i,j].ShortestPathFromStart <= StepsNeeded)
        {
          string fill = GardenMap[i,j].ShortestPathFromStart == 0 ? "blue" : (GardenMap[i,j].ShortestPathFromStart % 2 == StepsNeeded % 2 ? "#32CD32" : "lightgray");
          svgbuilder.Append("\t<g>\n");
          svgbuilder.Append($"\t\t<rect x=\"{rectX}\" y=\"{rectY}\" width=\"{rectSide}\" height=\"{rectSide}\" stroke=\"black\" fill=\"{fill}\" />\n");
          // svgbuilder.Append($"\t\t<text font-size=\"12px\" x={rectX} y={rectY}>{GardenMap[i,j].ShortestPathFromStart}</text>");
          svgbuilder.Append("\t</g>\n");
        }
      }
    }

    svgbuilder.Append("</svg>");

    string svg = svgbuilder.ToString();

    html += svg;
    html += "\n</body>\n</html>";

    File.WriteAllText($"svg/{id}.html", html);
  }
}