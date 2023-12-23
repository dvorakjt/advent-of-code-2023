static class InputParser
{
  public static List<Brick> GetSortedBricksFromInput(string[] input)
  {
    TrimInput(input);

    List<Brick> bricks = new();

    for(int i = 0; i < input.Length; i++)
    {
      string[] coordinates = input[i].Split('~');
      int[] startingCoordinates = coordinates[0].Split(',').Select(int.Parse).ToArray();
      int[] endingCoordinates = coordinates[1].Split(',').Select(int.Parse).ToArray();

      Brick brick = new
      (
        i + 1,
        startingCoordinates[0], 
        startingCoordinates[1], 
        startingCoordinates[2] - 1, 
        endingCoordinates[0],
        endingCoordinates[1],
        endingCoordinates[2] - 1
      );

      bricks.Add(brick);
    }

    bricks.Sort();

    return bricks;
  }

  private static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }
}