static class InputParser
{
  public static List<(Point Position, Speed Speed)> GetPointAndSpeedListFromInput(string[] input)
  {
    TrimInput(input);

    List<(Point Position, Speed Speed)> positionAndSpeedList = [];

    for(int i = 0; i < input.Length; i++)
    {
      string[] positionAndSpeed = input[i].Split(" @ ");
      double[] coordinates = positionAndSpeed[0].Split(", ").Select(double.Parse).ToArray();
      double[] xyzSpeed = positionAndSpeed[1].Split(", ").Select(double.Parse).ToArray();

      Point position = new(coordinates[0], coordinates[1]);
      Speed speed = new(xyzSpeed[0], xyzSpeed[1]);

      positionAndSpeedList.Add((position, speed));
    }

    return positionAndSpeedList;
  }

  public static void TrimInput(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }
}