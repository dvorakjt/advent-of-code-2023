class DesertMap
{
  private Dictionary<string, DesertMapNode> Nodes = new();

  public void AddNode(string identifier, string left, string right)
  {
    DesertMapNode n = new DesertMapNode(identifier, left, right);
    Nodes.Add(n.Identifier, n);
  }

  public int Navigate(string start, string end, string directions)
  {
    if(!Nodes.ContainsKey(start) || !Nodes.ContainsKey(end))
    {
      throw new KeyNotFoundException("The map does not contain either the start or end values.");
    }

    DesertMapNode currentNode = Nodes[start];
    int directionsIndex = 0;
    int totalSteps = 0;

    while(currentNode.Identifier != end)
    {   
      char direction = directions[directionsIndex];

      if(direction == 'L')
      {
        currentNode = Nodes[currentNode.Left];
      }
      else
      {
        currentNode = Nodes[currentNode.Right];
      }

      directionsIndex++;
      directionsIndex %= directions.Length;
      totalSteps++;

      if(directionsIndex == 0 && currentNode.Identifier == start)
      {
        throw new Exception("You're going in a circle.");
      }
    }

    return totalSteps;
  }
}