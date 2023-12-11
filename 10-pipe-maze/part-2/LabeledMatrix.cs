class LabeledMatrix
{
  private LabeledMatrixPoint[,] Matrix;
  private List<Point> Path;
  private List<Point> InnerPerimeterPoints = new();
  private string[] OriginalInput;
  public int InnerPointCount { get; private set; } = 0;

  public LabeledMatrix(int height, int width, List<Point> path, string[] originalInput)
  {
    Matrix = new LabeledMatrixPoint[height, width];
    OriginalInput = originalInput;

    Path = path;

    LabelPipes();
    FindAndLabelInnerPerimeterPoints();
    FloodFillFromInnerPerimeterPoints();
    CountInnerPoints();
  }

  private void LabelPipes()
  {
    for(int i = 0; i < Path.Count; i++)
    {
      Point point = Path[i];
      Matrix[point.Row, point.Column] = new LabeledMatrixPoint(Label.PIPE, i);
    }
  }

  private void FindAndLabelInnerPerimeterPoints()
  {
    LabeledMatrixPoint startingPoint = GetStartingPoint();


    if(startingPoint.PathIndex == null)
    {
      throw new Exception("Starting point PathIndex must not be null.");
    }

    int startingPointIndex = (int)startingPoint.PathIndex;
    int currentPointIndex = startingPointIndex;
    Direction innerBoundaryDirection = Direction.E;

    do {
      TravelToNextPipeAndLabelInnerPoints(ref currentPointIndex, ref innerBoundaryDirection);
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

  public void TravelToNextPipeAndLabelInnerPoints(ref int currentPointIndex, ref Direction innerBoundaryDirection)
  {
    int nextPointIndex = currentPointIndex + 1;
    if(nextPointIndex >= Path.Count) nextPointIndex = 0;

    Direction directionToNextPoint = GetDirection(Path[currentPointIndex], Path[nextPointIndex]);
    Direction nextInnerBoundaryDirection;

    //the path continues straight
    if(directionToNextPoint == Direction.N)
    {
      if(innerBoundaryDirection == Direction.E || innerBoundaryDirection == Direction.W)
      {
        nextInnerBoundaryDirection = innerBoundaryDirection;
      }
      else //the path turned
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        if(previousDirection == Direction.W && innerBoundaryDirection == Direction.N)
        {
          nextInnerBoundaryDirection = Direction.E;
        }
        else if(previousDirection == Direction.W && innerBoundaryDirection == Direction.S)
        {
          nextInnerBoundaryDirection = Direction.W;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.SW);
        }
        else if(previousDirection == Direction.E && innerBoundaryDirection == Direction.N)
        {
          nextInnerBoundaryDirection = Direction.W;
        }
        else 
        {
          nextInnerBoundaryDirection = Direction.E;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.SE);
        }
      }
      
    }
    else if(directionToNextPoint == Direction.S)
    {
      if(innerBoundaryDirection == Direction.E || innerBoundaryDirection == Direction.W)  
      {
        nextInnerBoundaryDirection = innerBoundaryDirection;
      }
      else
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        if(previousDirection == Direction.W && innerBoundaryDirection == Direction.S)
        {
          nextInnerBoundaryDirection = Direction.E;
        }
        else if(previousDirection == Direction.W && innerBoundaryDirection == Direction.N)
        {
          nextInnerBoundaryDirection = Direction.W;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.NW);
        }
        else if(previousDirection == Direction.E && innerBoundaryDirection == Direction.S)
        {
          nextInnerBoundaryDirection = Direction.W;
        }
        else 
        {
          nextInnerBoundaryDirection = Direction.E;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.NE);
        }
      }
    }
    else if(directionToNextPoint == Direction.W)
    {
      if(innerBoundaryDirection == Direction.N || innerBoundaryDirection == Direction.S)
      {
        nextInnerBoundaryDirection = innerBoundaryDirection;
      }
      else 
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.N && innerBoundaryDirection == Direction.W)
        {
          nextInnerBoundaryDirection = Direction.S;
        }
        else if(previousDirection == Direction.N && innerBoundaryDirection == Direction.E)
        {
          nextInnerBoundaryDirection = Direction.N;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.NE);
        }
        else if(previousDirection == Direction.S && innerBoundaryDirection == Direction.W)
        {
          nextInnerBoundaryDirection = Direction.N;
        }
        else 
        {
          nextInnerBoundaryDirection = Direction.S;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.SE);
        }
      }
    }
    else
    {
      if(innerBoundaryDirection == Direction.N || innerBoundaryDirection == Direction.S)
      {
        //outerBoundaryDirection is unchanged
        nextInnerBoundaryDirection = innerBoundaryDirection;
      }
      else
      {
        int previousPointIndex = currentPointIndex - 1;
        if(previousPointIndex < 0) previousPointIndex = Path.Count - 1;

        Direction previousDirection = GetDirection(Path[previousPointIndex], Path[currentPointIndex]);

        //you encountered a turn
        if(previousDirection == Direction.N && innerBoundaryDirection == Direction.E)
        {
          nextInnerBoundaryDirection = Direction.S;
        }
        else if(previousDirection == Direction.N && innerBoundaryDirection == Direction.W)
        {
          nextInnerBoundaryDirection = Direction.N;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.NW);
        }
        else if(previousDirection == Direction.S && innerBoundaryDirection == Direction.E)
        {
          nextInnerBoundaryDirection = Direction.N;
        }
        else 
        {
          nextInnerBoundaryDirection = Direction.S;
          SaveAndLabelInnerPoint(Path[nextPointIndex], Direction.SW);
        }
      }
    }

    SaveAndLabelInnerPoint(Path[nextPointIndex], nextInnerBoundaryDirection);

    currentPointIndex = nextPointIndex;
    innerBoundaryDirection = nextInnerBoundaryDirection;
  }

  //have to update this to be able to save and label points NE, NW, SE, SW
  private void SaveAndLabelInnerPoint(Point p, Direction direction)
  {
    Point innerPoint;

    switch(direction)
    {
      case Direction.N :
        innerPoint = new Point(p.Row - 1, p.Column);
        break;
      case Direction.S :
        innerPoint = new Point(p.Row + 1, p.Column);
        break;
      case Direction.E :
        innerPoint = new Point(p.Row, p.Column + 1);
        break;
      case Direction.W : 
        innerPoint = new Point(p.Row, p.Column - 1);
        break;
      case Direction.NE :
        innerPoint = new Point(p.Row - 1, p.Column + 1);
        break;
      case Direction.NW :
        innerPoint = new Point(p.Row - 1, p.Column - 1);
        break;
      case Direction.SE :
        innerPoint = new Point(p.Row + 1, p.Column + 1);
        break;
      default:
        innerPoint = new Point(p.Row + 1, p.Column - 1);
        break;
    }

    if(Matrix[innerPoint.Row, innerPoint.Column] == null)
    {
      Matrix[innerPoint.Row, innerPoint.Column] = new LabeledMatrixPoint(Label.INNER);
      InnerPerimeterPoints.Add(innerPoint);
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

  private void FloodFillFromInnerPerimeterPoints()
  {
    foreach(Point p in InnerPerimeterPoints)
    {
      FloodFillFromInnerPerimeterPoint(p);
    }
  }

  private void FloodFillFromInnerPerimeterPoint(Point p)
  {
    FloodFillFromInnerPerimeterPoint(p, new HashSet<Point>());
  }

  private void FloodFillFromInnerPerimeterPoint(Point p, HashSet<Point> seenPoints)
  {
    if(seenPoints.Contains(p)) return;

    if(
      p.Row < 0 || 
      p.Column < 0 ||
      p.Row >= Matrix.GetLength(0) ||
      p.Column >= Matrix.GetLength(1) ||
      (Matrix[p.Row, p.Column] != null && Matrix[p.Row, p.Column].Label == Label.PIPE)
    )
    {
      return;
    }

    Matrix[p.Row, p.Column] = new LabeledMatrixPoint(Label.INNER);
    seenPoints.Add(p);

    FloodFillFromInnerPerimeterPoint(new Point(p.Row - 1, p.Column), seenPoints);
    FloodFillFromInnerPerimeterPoint(new Point(p.Row + 1, p.Column), seenPoints);
    FloodFillFromInnerPerimeterPoint(new Point(p.Row, p.Column - 1), seenPoints);
    FloodFillFromInnerPerimeterPoint(new Point(p.Row, p.Column + 1), seenPoints);
  }

  private void CountInnerPoints()
  {
    for(int i = 0; i < Matrix.GetLength(0); i++)
    {
      for(int j = 0; j < Matrix.GetLength(1); j++)
      {
        if(Matrix[i,j] != null && Matrix[i,j].Label == Label.INNER) InnerPointCount++;
      }
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
        lines[i] += Matrix[i,j] == null ? "_" : Matrix[i,j].Label == Label.PIPE ? OriginalInput[i][j] : "I";
      }
    }

    File.WriteAllLines("output.txt", lines);
  }
}