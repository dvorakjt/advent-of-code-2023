using System.Text;

class PathIntersectionDetector
{
  private readonly (double Min, double Max) SearchArea = (2e14, 4e14);
  private readonly List<LineSegment> LineSegments = [];

  public int PathIntersections { get; private set; } = 0;

  public PathIntersectionDetector(List<(Point Position, Speed Speed)> positionAndSpeedList)
  {
    ExtrapolateLineSegments(positionAndSpeedList);
    CountPathIntersections();
    //Uncomment to save a new SVG visualization
    //SaveAllLineSegmentsAsSVG();
  }

  private void ExtrapolateLineSegments(List<(Point Position, Speed Speed)> positionAndSpeedList)
  {
    foreach(var positionAndSpeed in positionAndSpeedList)
    {
      try {
        LineSegment lineSegment = CreateLineSegmentFromPointAndSpeed(positionAndSpeed.Position, positionAndSpeed.Speed);
        LineSegments.Add(lineSegment);
        foreach(var point in lineSegment.EndPoints)
        {
          if(!IsWithinBounds(point))
          {
            Console.WriteLine($"{point.X}, {point.Y}");
          }
        }
      }
      catch(SearchAreaNotReachedException) {}
    }
  }
  
  private LineSegment CreateLineSegmentFromPointAndSpeed(Point point, Speed speed)
  {
    LineSegment lineSegment = new();
    lineSegment.EndPoints[0] = ProjectPointToBoundary(point, speed);
    lineSegment.EndPoints[1] =
      IsWithinBounds(point) ? point : 
      ProjectPointToBoundary(lineSegment.EndPoints[0], new Speed(speed.X * -1, speed.Y * -1));
    return lineSegment;
  }

  private Point ProjectPointToBoundary(Point point, Speed speed)
  {
    if(speed.X == 0 && speed.Y == 0)
    {
      throw new SpeedZeroException("A point with speed.X and speed.Y equal to 0 cannot be projected to a boundary.");
    }

    if(point.X < SearchArea.Min && speed.X <= 0)
    {
      throw new SearchAreaNotReachedException($"The coordinates ({point.X}, {point.Y}) traveling at ({speed.X}, {speed.Y}) will never reach the search area.");
    }
    if(point.X > SearchArea.Max && speed.X >= 0)
    {
      throw new SearchAreaNotReachedException($"The coordinates ({point.X}, {point.Y}) traveling at ({speed.X}, {speed.Y}) will never reach the search area.");
    }
    if(point.Y < SearchArea.Min && speed.Y <= 0)
    {
      throw new SearchAreaNotReachedException($"The coordinates ({point.X}, {point.Y}) traveling at ({speed.X}, {speed.Y}) will never reach the search area.");
    }
    if(point.Y > SearchArea.Max && speed.Y >= 0)
    {
      throw new SearchAreaNotReachedException($"The coordinates ({point.X}, {point.Y}) traveling at ({speed.X}, {speed.Y}) will never reach the search area.");
    }

    double xBoundary = speed.X > 0 ? SearchArea.Max : SearchArea.Min;
    double yBoundary = speed.Y > 0 ? SearchArea.Max : SearchArea.Min;

    if(speed.X == 0)
    {
      return new Point(point.X, yBoundary);
    }
    if(speed.Y == 0)
    {
      return new Point(xBoundary, point.Y);
    }

    double distanceToXBoundary = xBoundary - point.X;
    double distanceToYBoundary = yBoundary - point.Y;

    double yAtXBoundary = 
      Math.Abs(distanceToXBoundary * (speed.Y / speed.X)) * (speed.Y > 0 ? 1 : -1) + point.Y;
      
    double xAtYBoundary = 
      Math.Abs(distanceToYBoundary * (speed.X / speed.Y)) * (speed.X > 0 ? 1 : -1) + point.X;
  
    if(IsWithinBounds(yAtXBoundary))
    {
      return new Point(xBoundary, yAtXBoundary);
    }
    else 
    {
      return new Point(xAtYBoundary, yBoundary);
    }
  }

  private bool IsWithinBounds(double coordinate)
  {
    return coordinate >= SearchArea.Min && coordinate <= SearchArea.Max;
  }

  private bool IsWithinBounds(Point point)
  {
    return IsWithinBounds(point.X) && IsWithinBounds(point.Y);
  }
  
  private void CountPathIntersections()
  {
    for(int i = 0; i < LineSegments.Count - 1; i++)
    {
      for(int j = i + 1; j < LineSegments.Count; j++)
      {
        if(PathsIntersect(LineSegments[i], LineSegments[j]))
        {
          PathIntersections++;
        }
      }
    }
  }

  private bool PathsIntersect(LineSegment a, LineSegment b)
  {
    Orientation[] orientations =
    [
      GetOrientation(a.EndPoints[0], a.EndPoints[1], b.EndPoints[0]),
      GetOrientation(a.EndPoints[0], a.EndPoints[1], b.EndPoints[1]),
      GetOrientation(b.EndPoints[0], b.EndPoints[1], a.EndPoints[0]),
      GetOrientation(b.EndPoints[0], b.EndPoints[1], a.EndPoints[1]),
    ];
   
    if(orientations[0] == Orientation.COLLINEAR)
    {
      return CollinearLineSegmentsOverlap(a.EndPoints[0], a.EndPoints[1], b.EndPoints[0]);
    }
    if(orientations[1] == Orientation.COLLINEAR)
    {
      return CollinearLineSegmentsOverlap(a.EndPoints[0], a.EndPoints[1], b.EndPoints[1]);
    }
    if(orientations[2] == Orientation.COLLINEAR)
    {
      return CollinearLineSegmentsOverlap(b.EndPoints[0], b.EndPoints[1], a.EndPoints[0]);
    }
    if(orientations[3] == Orientation.COLLINEAR)
    {
      return CollinearLineSegmentsOverlap(b.EndPoints[0], b.EndPoints[1], a.EndPoints[1]);
    }

    return orientations[0] != orientations[1] && orientations[2] != orientations[3];
  }

  private static Orientation GetOrientation(Point p, Point q, Point r)
  {
    double result =  (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
    if(result > 0)
    {
      return Orientation.CLOCKWISE;
    }
    else if(result < 0)
    {
      return Orientation.COUNTERCLOCKWISE;
    }

    return Orientation.COLLINEAR;
  }

  private static bool CollinearLineSegmentsOverlap(Point p, Point q, Point r)
  {
    return 
      r.X <= Math.Max(p.X, q.X) && 
      r.X >= Math.Min(p.X, q.X) && 
      r.Y <= Math.Min(p.Y, q.Y) && 
      r.Y >= Math.Min(p.Y, q.Y);
  }

  private void SaveAllLineSegmentsAsSVG()
  {
    double scaleAdjustment = 1e12;
    double min = SearchArea.Min / scaleAdjustment;
    double max = SearchArea.Max / scaleAdjustment;

    StringBuilder stringBuilder = new($"<svg viewBox=\"0 0 {max + min} {max + min}\" xmlns=\"http://www.w3.org/2000/svg\">\n");
    
    foreach(var lineSegment in LineSegments)
    {
      string svgLine = CreateSVGLineElement(lineSegment, scaleAdjustment);
      stringBuilder.Append(svgLine);
    }

    stringBuilder.Append("</svg>");

    File.WriteAllText("visualization.svg", stringBuilder.ToString());
  }

  private string CreateSVGLineElement(LineSegment lineSegment, double scaleAdjustment)
  {
    double x1 = lineSegment.EndPoints[0].X / scaleAdjustment;
    double x2 = lineSegment.EndPoints[1].X / scaleAdjustment;
    double y1 = lineSegment.EndPoints[0].Y / scaleAdjustment;
    double y2 = lineSegment.EndPoints[1].Y / scaleAdjustment;

    return $"\t<line x1=\"{x1}\" x2=\"{x2}\" y1=\"{y1}\" y2=\"{y2}\" stroke=\"black\" />\n";
  }
}