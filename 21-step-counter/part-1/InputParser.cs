static class InputParser
{
  public static ReachablePlotFinder CreateReachablePlotFinderFromInput(string[] input, int stepsNeeded)
  {
    TrimInput(input);

    Tile[,] gardenMap = new Tile[input.Length, input[0].Length];

    (int Row, int Column) startingLocation = FindStartingLocation(input);

    for(int row = 0; row < input.Length; row++)
    {
      for(int column = 0; column < input[0].Length; column++)
      {
        bool[] reachableNeighbors =
        [
          row - 1 >= 0 && gardenMap[row - 1, column].IsReachable,
          column -1 >= 0 && gardenMap[row, column - 1].IsReachable,
          row + 1 < input.Length && input[row + 1][column] != '#',
          column + 1 < input[row].Length && input[row][column + 1] != '#',
        ];
        
        gardenMap[row, column] = new Tile(row, column, input[row][column] != '#' && reachableNeighbors.Any(n => n), input[row][column] == 'S' ? 0 : int.MaxValue);
      }
    }

    return new ReachablePlotFinder(gardenMap, stepsNeeded);
  }

  private static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }

  private static (int Row, int Column) FindStartingLocation(string[] input)
  {
    for(int row = 0; row < input.Length; row++)
    {
      for(int column = 0; column < input[0].Length; column++)
      {
        if(input[row][column] == 'S')
        {
          return (row, column);
        }
      }
    }

    throw new InvalidOperationException("The input must contain a tile labeled 'S'");
  }
}