readonly struct Neighbor(Point point, Direction directionFromOther, int distanceFromOther)
{
  public readonly Point Point = point;
  public readonly Direction DirectionFromOther = directionFromOther;
  public readonly int DistanceFromOther = distanceFromOther;
}