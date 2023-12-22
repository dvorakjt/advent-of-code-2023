static class InputParser
{
  public static Platform ParseInput(string[] input)
  {
    TrimInput(input);

    Platform matrix = new(input.Length, input[0].Length);

    for(int i = 0; i < input.Length; i++)
    {
      for(int j = 0; j < input[i].Length; j++)
      {
        if(input[i][j] == 'O')
        {
          matrix.AddRoundRock(i, j);
        }
        else if(input[i][j] == '#')
        {
          matrix.AddSquareRock(i, j);
        }
      }
    }

    return matrix;
  }

  private static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }
}