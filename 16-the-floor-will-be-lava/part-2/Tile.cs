class Tile
{
  public char Content { get; private set; }
  public bool Energized 
  { 
    get {
      return EnteredFrom.Count > 0;
    } 
  }

  public HashSet<Direction> EnteredFrom = new();

  public Tile(char content)
  {
    Content = content;
  }

  public void Visit(Direction enteredFrom) {
    EnteredFrom.Add(enteredFrom);
  } 
}