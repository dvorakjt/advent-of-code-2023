class DesertMapNode
{
  public string Identifier { get; private set; }
  public string Left { get; private set; }
  public string Right { get; private set; }

  public DesertMapNode (string identifier, string left, string right)
  {
    Identifier = identifier;
    Left = left;
    Right = right;
  }

  public bool IsStartingNode()
  {
    return Identifier[2] == 'A';
  }

  public bool IsDestinationNode()
  {
    return Identifier[2] == 'Z';
  }
}