class Beam 
{
  public Point Position { get; private set; }
  public Direction Direction { get; private set; }
  public Beam (Point position, Direction direction)
  {
    Position = position;
    Direction = direction;
  }

  public void Move() 
  {
    switch(Direction) {
      case Direction.NORTH :
        Position = new Point(Position.Row - 1, Position.Column);
        break;
      case Direction.SOUTH :
        Position = new Point(Position.Row + 1, Position.Column);
        break;
      case Direction.EAST :
        Position = new Point(Position.Row, Position.Column + 1);
        break;
      case Direction.WEST :
        Position = new Point(Position.Row, Position.Column - 1);
        break;
    }
  }

  public void Reflect(char reflector)
  {
    switch(Direction) {
      case Direction.NORTH :
        if(reflector == '/') 
        {
          Direction = Direction.EAST;
        }
        else
        {
          Direction = Direction.WEST;
        }
        break;
      case Direction.SOUTH :
        if(reflector == '/') 
        {
          Direction = Direction.WEST;
        }
        else
        {
          Direction = Direction.EAST;
        }
        break;
      case Direction.EAST :
        if(reflector == '/') 
        {
          Direction = Direction.NORTH;
        }
        else
        {
          Direction = Direction.SOUTH;
        }
        break;
      case Direction.WEST :
        if(reflector == '/') 
        {
          Direction = Direction.SOUTH;
        }
        else
        {
          Direction = Direction.NORTH;
        }
        break;
    }
  }

  public Beam? Split(char splitter)
  {
    if((Direction == Direction.NORTH || Direction == Direction.SOUTH) && splitter == '-')
    {
      Direction = Direction.WEST;
      return new Beam(Position, Direction.EAST);
    }
    else if ((Direction == Direction.EAST || Direction == Direction.WEST) && splitter == '|')
    {
      Direction = Direction.NORTH;
      return new Beam(Position, Direction.SOUTH);
    }
    
    return null;
  }
}