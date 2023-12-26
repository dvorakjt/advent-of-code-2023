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

        if(tile.ShortestPathFromStart == 0)
        {
          UnvisitedTiles.InsertOrUpdate(tile);
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
      Tile tile = UnvisitedTiles.ExtractMin();
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
        UnvisitedTiles.InsertOrUpdate(neighbor);
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
}