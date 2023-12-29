static class InputParser
{
  public static CruciblePathGraph CreateCruciblePathGraphFromInput(string[] input)
  {
    TrimInput(input);

    int[,] heatLossMap = CreateHeatLossMapFromInput(input);

    return new CruciblePathGraph(heatLossMap);
  }

  private static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }

  private static int[,] CreateHeatLossMapFromInput(string[] input)
  {
    int[,] heatLossMap = new int[input.Length, input[0].Length];

    for(int i = 0; i < input.Length; i++)
    {
      for(int j = 0; j < input[0].Length; j++)
      {
        heatLossMap[i,j] = input[i][j] - '0';
      }
    }

    return heatLossMap;
  }
}