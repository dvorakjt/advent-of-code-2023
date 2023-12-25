static class InputParser
{
  public static PathFinder CreatePathFinderFromInput(string[] input)
  {
    TrimInput(input);

    bool[,] forestMap = new bool[input.Length, input[0].Length];

    for(int row = 0; row < input.Length; row++)
    {
      for(int column = 0; column < input[0].Length; column++)
      {
        forestMap[row,column] = input[row][column] != '#';
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