struct Brick : IComparable<Brick>
{
  public int Id;
  public int X1;
  public int X2;
  public int Y1;
  public int Y2;
  public int Z1;

  public int Height;

  public Brick(int id, int x1, int y1, int z1, int x2, int y2, int z2)
  {
    Id = id;
    X1 = x1;
    X2 = x2;
    Y1 = y1;
    Y2 = y2;
    Z1 = z1;
    Height = z2 + 1 - Z1;
  }

  public int CompareTo(Brick other)
  {
    return Z1 - other.Z1;
  }
}