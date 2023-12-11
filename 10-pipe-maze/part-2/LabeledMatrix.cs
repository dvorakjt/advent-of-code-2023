class LabeledMatrix
{
  private LabeledMatrixPoint[,] Matrix;
  private List<Point> Path;
  public int InnerPointCount { get; private set; } = 0;

  public LabeledMatrix(int height, int width, List<Point> path)
  {
    Matrix = new LabeledMatrixPoint[height, width];
    
    Path = path;

    LabelPipes();
    CountAndLabelInnerPoints();
  }

  private void LabelPipes()
  {
    for(int i = 0; i < Path.Count; i++)
    {
      Point point = Path[i];
      Matrix[point.Row, point.Column] = new LabeledMatrixPoint(Label.PIPE, i);
    }
  }

  private void CountAndLabelInnerPoints()
  {
    LabeledMatrixPoint startingPoint = GetStartingPoint();


    if(startingPoint.PathIndex == null)
    {
      throw new Exception("Starting point PathIndex must not be null.");
    }

    int startingPointIndex = (int)startingPoint.PathIndex;
    int currentPointIndex = startingPointIndex;
    Direction outerBoundaryDirection = Direction.E;

    

    do {
      Console.WriteLine(outerBoundaryDirection);
      Console.WriteLine(Path[currentPointIndex].Row + ", " + Path[currentPointIndex].Column);
      TravelToNextPipeAndLabelInnerPoints(ref currentPointIndex, ref outerBoundaryDirection);
    } while(currentPointIndex != startingPointIndex);
  }

  private LabeledMatrixPoint GetStartingPoint()
  {
    for(int i = 0; i < Matrix.GetLength(0); i++)
    {
      for(int j = 0; j < Matrix.GetLength(1); j++)
      {
        if(Matrix[i,j] != null) return Matrix[i,j];
      }
    }

    throw new Exception("Starting point not found");
  }

  public void TravelToNextPipeAndLabelInnerPoints(ref int currentPointIndex, ref Direction outerBoundaryDirection)
  {
    int nextPointIndex = currentPointIndex + 1;
    if(nextPointIndex >= Path.Count) nextPointIndex = 0;

    Direction directionToNextPoint = GetDirection(Path[currentPointIndex], Path[nextPointIndex]);
    Direction nextOuterBoundaryDirection;

    if(directionToNextPoint == Direction.N)
    {
      if(outerBoundaryDirection == Direction.E || outerBoundaryDirection == Direction.W)
      {
        //outerBoundaryDirection is unchanged
        nextOuterBoundaryDirection = outerBoundaryDirection;
      }
      else
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.W && outerBoundaryDirection == Direction.S)
        {
          nextOuterBoundaryDirection = Direction.W;
        }
        else if(previousDirection == Direction.W && outerBoundaryDirection == Direction.N)
        {
          nextOuterBoundaryDirection = Direction.E;
        }
        else if(previousDirection == Direction.E && outerBoundaryDirection == Direction.S)
        {
          nextOuterBoundaryDirection = Direction.E;
        }
        else //if(previousDirection == Direction.E && outerBoundaryDirection == Direction.N)
        {
          nextOuterBoundaryDirection = Direction.W;
        }
      }
      
    }
    else if(directionToNextPoint == Direction.S)
    {
      if(outerBoundaryDirection == Direction.E || outerBoundaryDirection == Direction.W)  
      {
        //outerBoundaryDirection is unchanged
        nextOuterBoundaryDirection = outerBoundaryDirection;
      }
      else
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.W && outerBoundaryDirection == Direction.N)
        {
          nextOuterBoundaryDirection = Direction.W;
        }
        else if(previousDirection == Direction.W && outerBoundaryDirection == Direction.S)
        {
          nextOuterBoundaryDirection = Direction.E;
        }
        else if(previousDirection == Direction.E && outerBoundaryDirection == Direction.N)
        {
          nextOuterBoundaryDirection = Direction.E;
        }
        else
        {
          nextOuterBoundaryDirection = Direction.W;
        }
      }
    }
    else if(directionToNextPoint == Direction.W)
    {
      if(outerBoundaryDirection == Direction.N || outerBoundaryDirection == Direction.S)
      {
        //outerBoundaryDirection is unchanged
        nextOuterBoundaryDirection = outerBoundaryDirection;
      }
      else 
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.N && outerBoundaryDirection == Direction.E)
        {
          nextOuterBoundaryDirection = Direction.N;
        }
        else if(previousDirection == Direction.N && outerBoundaryDirection == Direction.W)
        {
          nextOuterBoundaryDirection = Direction.S;
        }
        else if(previousDirection == Direction.S && outerBoundaryDirection == Direction.E)
        {
          nextOuterBoundaryDirection = Direction.S;
        }
        else
        {
          nextOuterBoundaryDirection = Direction.N;
        }
      }
    }
    else
    {
      if(outerBoundaryDirection == Direction.N || outerBoundaryDirection == Direction.S)
      {
        //outerBoundaryDirection is unchanged
        nextOuterBoundaryDirection = outerBoundaryDirection;
      }
      else
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.N && outerBoundaryDirection == Direction.W)
        {
          nextOuterBoundaryDirection = Direction.N;
        }
        else if(previousDirection == Direction.N && outerBoundaryDirection == Direction.E)
        {
          nextOuterBoundaryDirection = Direction.S;
        }
        else if(previousDirection == Direction.S && outerBoundaryDirection == Direction.W)
        {
          nextOuterBoundaryDirection = Direction.S;
        }
        else
        {
          nextOuterBoundaryDirection = Direction.N;
        }
      }
    }

    LabelInnerPoint(Path[nextPointIndex], nextOuterBoundaryDirection);

    currentPointIndex = nextPointIndex;
    outerBoundaryDirection = nextOuterBoundaryDirection;
  }

  private void LabelInnerPoint(Point p, Direction outerBoundaryDirection)
  {
    Point innerPoint;

    switch(outerBoundaryDirection)
    {
      case Direction.N :
        innerPoint = new Point(p.Row + 1, p.Column);
        break;
      case Direction.S :
        innerPoint = new Point(p.Row - 1, p.Column);
        break;
      case Direction.E :
        innerPoint = new Point(p.Row, p.Column - 1);
        break;
      default:
        innerPoint = new Point(p.Row, p.Column + 1);
        break;
    }

    if(Matrix[innerPoint.Row, innerPoint.Column] == null)
    {
      Matrix[innerPoint.Row, innerPoint.Column] = new LabeledMatrixPoint(Label.INNER);
      InnerPointCount++;
    }
  }

  private Direction GetDirection(Point origin, Point destination)
  {
    if(destination.Row < origin.Row)
    {
      return Direction.N;
    }
    else if(destination.Row > origin.Row)
    {
      return Direction.S;
    }
    else if(destination.Column < origin.Column)
    {
      return Direction.W;
    }
    else{
      return Direction.E;
    }
  }

  public void Save()
  {
    string[] lines = new string[Matrix.GetLength(0)];

    for(int i = 0; i < Matrix.GetLength(0); i++)
    {
      lines[i] = "";

      for(int j = 0; j < Matrix.GetLength(1); j++)
      {
        lines[i] += Matrix[i,j] == null ? "_" : Matrix[i,j].Label == Label.PIPE ? "P" : "I";
      }
    }

    File.WriteAllLines("output.txt", lines);
  }
}