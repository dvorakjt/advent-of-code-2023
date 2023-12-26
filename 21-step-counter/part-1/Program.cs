//parse input into 129 x  129 matrix
string[] input = File.ReadAllLines("input.txt");

for(int i = 0; i < input.Length; i++)
{
  input[i] = input[i].Trim();
}

Tile[,] gardenMap = new Tile[129,129];

//find starting location
int startRow = -1;
int startCol = -1;

for(int i = 0; i < input.Length; i++)
{
  for(int j = 0; j < input[0].Length; j++)
  {
    if(input[i][j] == 'S')
    {
      startRow = i;
      startCol = j;
      break;
    }
  }
}

int row = 0;

//mark squares visitable that fall in a diamond around the center
for(int i = startRow - 64; i <= startRow + 64; i++, row++)
{
  int column = 0;
  int unreachableTileCount = Math.Abs(row - 64);

  for(int j = startCol - 64; j <= startCol + 64; j++, column++)
  {
    bool isReachable = column >= unreachableTileCount && column < 129 - unreachableTileCount;
    gardenMap[row, column] = new Tile(row, column, isReachable && input[i][j] != '#', input[i][j] == 'S' ? 0 : int.MaxValue);
  }
}


//use djikstra's algorithm to the find the shortest path to each tile
MinHeap<Tile> unvisitedTiles = new();

foreach(var tile in gardenMap)
{
  if(tile.CanBeVisited) unvisitedTiles.Insert(tile);
}

while(unvisitedTiles.Count > 0)
{
  Tile currentTile = unvisitedTiles.Extract();
  currentTile.CanBeVisited = false;

  if(currentTile.Row - 1 >= 0)
  {
    Tile northernNeighbor = gardenMap[currentTile.Row - 1, currentTile.Column];
    if(northernNeighbor.CanBeVisited)
    {
      northernNeighbor.MinSteps = Math.Min(currentTile.MinSteps + 1, northernNeighbor.MinSteps);
      unvisitedTiles.DecreaseKey(northernNeighbor);
    }
  }

  if(currentTile.Row + 1 < gardenMap.GetLength(0))
  {
    Tile southernNeighbor = gardenMap[currentTile.Row + 1, currentTile.Column];
    if(southernNeighbor.CanBeVisited)
    {
      southernNeighbor.MinSteps = Math.Min(currentTile.MinSteps + 1, southernNeighbor.MinSteps);
      unvisitedTiles.DecreaseKey(southernNeighbor);
    }
  }

  if(currentTile.Column - 1 >= 0)
  {
    Tile westernNeighbor = gardenMap[currentTile.Row, currentTile.Column - 1];
    if(westernNeighbor.CanBeVisited)
    {
      westernNeighbor.MinSteps = Math.Min(currentTile.MinSteps + 1, westernNeighbor.MinSteps);
      unvisitedTiles.DecreaseKey(westernNeighbor);
    }
  }

  if(currentTile.Column + 1 < gardenMap.GetLength(1))
  {
    Tile easternNeighbor = gardenMap[currentTile.Row, currentTile.Column + 1];
    if(easternNeighbor.CanBeVisited)
    {
      easternNeighbor.MinSteps = Math.Min(currentTile.MinSteps + 1, easternNeighbor.MinSteps);
      unvisitedTiles.DecreaseKey(easternNeighbor);
    }
  }
}

int reachableTiles = 0;

for(int i = 0; i < gardenMap.GetLength(0); i++)
{
  for(int j = 0; j < gardenMap.GetLength(1); j++)
  {
    if(gardenMap[i,j].MinSteps <= 64 && gardenMap[i,j].MinSteps % 2 == 0)
    {
      reachableTiles++;
    }
  }
}

// int svgViewBoxMax = 150 * 50;

// string svgOpeningTag = $"<svg viewBox=\"0 0 {svgViewBoxMax} {svgViewBoxMax}\" xmlns=\"http://www.w3.org/2000/svg\">\n";

// foreach(var tile in gardenMap)
// {
//   if(tile.MinSteps <= 64)
//   {
//     svgOpeningTag += $"\t<rect width=\"50\" height=\"50\" x=\"{tile.Column * 50}\" y=\"{(128 - tile.Row) * 50}\" fill=\"transparent\" stroke=\"black\" />\n";
//     svgOpeningTag += $"\t<text dx=\"33%\" dy=\"33%\" fill=\"black\">{tile.MinSteps}</text>\n";
//   }
// }

// svgOpeningTag += "</svg>";

// File.WriteAllText("visualization.svg", svgOpeningTag);

Console.WriteLine(reachableTiles);