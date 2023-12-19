struct Point 
{
  public int Row { get; private set; }
  public int Column { get; private set; }

  public Point(int row, int column)
  {
    Row = row;
    Column = column;
  }
}