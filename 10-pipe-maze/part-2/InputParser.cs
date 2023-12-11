static class InputParser
{
  public static Maze CreateMazeFromInput(string[] input)
  {
    //first trim all new line characters
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }

    int rows = input.Length;
    int columns = input[0].Length;

    Tile[,] tiles = new Tile[rows, columns];
    Point startingPoint = new Point(-1, -1);

    for(int row = 0; row < rows; row++)
    {
      for(int col = 0; col < columns; col++)
      {
        char tile = input[row][col];
        tiles[row,col] = new Tile(tile);

        if(tile == 'S')
        {
          startingPoint = new Point(row, col);
        }
      }
    }

    if(startingPoint.Row == -1 || startingPoint.Column == -1)
    {
      throw new InvalidDataException("Could not find a starting point within the maze.");
    }

    return new Maze(tiles, startingPoint);
  }
}