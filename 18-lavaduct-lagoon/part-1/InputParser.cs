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
    char direction = inputLine[0];

    switch(direction)
    {
      case 'U':
        return Direction.UP;
      case 'D' :
        return Direction.DOWN;
      case 'L' :
        return Direction.LEFT;
      case 'R' :
        return Direction.RIGHT;
      default :
        throw new ArgumentException("Input line should begin with 'U', 'D', 'L', or 'R'.");
    }
  }

  private static int GetDistance(string inputLine)
  {
    return int.Parse(inputLine.Split(' ')[1]);
  }
}