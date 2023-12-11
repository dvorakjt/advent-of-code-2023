class LoopMap
{
  private List<Point> LoopPath;
  private char[,] Matrix;
  public int EnclosedArea { get; private set; }

  public LoopMap(int height, int width, List<Point> loopPath)
  {
    LoopPath = loopPath;
    Matrix = new char[height,width];

    PopulateMatrix();
    
    EnclosedArea = GetEnclosedArea();
  }

  private void PopulateMatrix()
  {
    foreach(Point point in LoopPath)
    {
      Matrix[point.Row,point.Column] = 'L';
    }
  }

  private int GetEnclosedArea()
  {
    HashSet<Point> unenclosedPoints = new();

    //scan left to right and right to left
    for(int row = 0; row < Matrix.GetLength(0); row++)
    {
      int left = 0;

      for(; left < Matrix.GetLength(1); left++)
      {
        if(Matrix[row, left] == 'L') break;

        unenclosedPoints.Add(new Point(row, left));
      }

      for(int right = Matrix.GetLength(1) - 1; right > left; right--)
      {
        if(Matrix[row, right] == 'L') break;

        unenclosedPoints.Add(new Point(row, right));
      }
    }


    //scan top to bottom and bottom to top
    for(int column = 0; column < Matrix.GetLength(1); column++)
    {
      int top = 0;

      for(; top < Matrix.GetLength(0); top++)
      {
        if(Matrix[top, column] == 'L') break;

        unenclosedPoints.Add(new Point(top, column));
      }

      for(int bottom = Matrix.GetLength(0) - 1; bottom > top; bottom--)
      {
        if(Matrix[bottom, column] == 'L') break;

        unenclosedPoints.Add(new Point(bottom, column));
      }
    }

    int totalArea = Matrix.GetLength(0) * Matrix.GetLength(1);

    return totalArea - LoopPath.Count - unenclosedPoints.Count;
  }
}