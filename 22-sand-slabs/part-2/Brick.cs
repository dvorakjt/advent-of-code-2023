struct Brick : IComparable<Brick>
{
  public int Id { get; private set; }
  public int X1 { get; private set; }
  public int X2 { get; private set; }
  public int Y1 { get; private set; }
  public int Y2 { get; private set; }
  public int Z1 { get; private set; }
  public int Z2 { get; private set; }

  public int Height 
  {
    get 
    {
      return Z2 + 1 - Z1;
    }
  }

  public Brick(int id, int x1, int y1, int z1, int x2, int y2, int z2)
  {
    Id = id;
    X1 = x1;
    X2 = x2;
    Y1 = y1;
    Y2 = y2;
    Z1 = z1;
    Z2 = z2;
  }

  public int CompareTo(Brick other)
  {
    return Z1 - other.Z1;
  }
}