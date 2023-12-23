static class InputParser
{
  public static Lavaduct ParseInput(string[] input)
  {
    Lavaduct lavaduct = new();

    int locationX = 0;
    int locationY = 0;

    Direction directionToPrevious = GetDirection(input[^2]);
    Direction directionToCurrent = GetDirection(input[^1]);
    Direction directionToNext = GetDirection(input[0]);

    PerimeterTile firstTile = new(locationX, locationY, directionToPrevious, directionToCurrent, directionToNext);
    lavaduct.AddPerimeterTile(firstTile);

    for(int i = 0; i < input.Length - 1; i++)
    {
      directionToPrevious = directionToCurrent;
      directionToCurrent = directionToNext;
      directionToNext = GetDirection(input[i + 1]);

      int distance = GetDistance(input[i]);

      switch(directionToCurrent)
      {
        case Direction.UP:
          locationY -= distance;
          break;
        case Direction.DOWN:
          locationY += distance;
          break;
        case Direction.LEFT:
          locationX -= distance;
          break;
        case Direction.RIGHT:
          locationX += distance;
          break;
      }

      PerimeterTile perimeterTile = new(locationX, locationY, directionToPrevious, directionToCurrent, directionToNext);
      lavaduct.AddPerimeterTile(perimeterTile);
    }

    PerimeterTile lastTile = new(firstTile);
    lavaduct.AddPerimeterTile(lastTile);

    return lavaduct;
  }


  private static Direction GetDirection(string inputLine)
  {
    char direction = inputLine[^2];

    switch(direction)
    {
      case '0':
        return Direction.RIGHT;
      case '1' :
        return Direction.DOWN;
      case '2' :
        return Direction.LEFT;
      case '3' :
        return Direction.UP;
      default :
        throw new ArgumentException("The second to last character of each input line must be a number between 0 and 3 (inclusive).");
    }
  }

  private static int GetDistance(string inputLine)
  {
    string hexadecimalDistance = inputLine[(inputLine.IndexOf('#') + 1)..^2];
    int result = Convert.ToInt32(hexadecimalDistance, 16);
    return result;
  }
}