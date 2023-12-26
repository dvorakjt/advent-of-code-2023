static class InputParser
{
  public static ReachablePlotFinder CreateReachablePlotFinderFromInput(string[] input, int stepsNeeded)
  {
    TrimInput(input);

    Tile[,] gardenMap = new Tile[input.Length, input[0].Length];

    for(int row = 0; row < input.Length; row++)
    {
      for(int column = 0; column < input[0].Length; column++)
      {
        gardenMap[row, column] = new Tile(row, column, input[row][column] != '#', input[row][column] == 'S' ? 0 : int.MaxValue);
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
}