static class InputParser
{
  public static PathFinder CreatePathFinderFromInput(string[] input)
  {
    TrimInput(input);

    Tile[,] forestMap = new Tile[input.Length, input[0].Length];

    for(int row = 0; row < input.Length; row++)
    {
      for(int column = 0; column < input[row].Length; column++)
      {
        var tileType = input[row][column] switch
        {
          '#' => TileType.FOREST,
          '>' => TileType.EASTWARD_SLOPE,
          '<' => TileType.WESTWARD_SLOPE,
          '^' => TileType.NORTHWARD_SLOPE,
          'v' => TileType.SOUTHWARD_SLOPE,
          _ => TileType.PATH,
        };

        forestMap[row, column] = new Tile(tileType);
      }
    }

    return new PathFinder(forestMap);
  }

  private static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }
}