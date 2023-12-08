class Navigator
{
  private Dictionary<string, DesertMapNode> Nodes;
  private DesertMapNode CurrentNode;
  private string Directions;
  private int DirectionsIndex = 0;
  public long Steps { get; private set; } = 0;


  public Navigator
  (
    Dictionary<string, DesertMapNode> nodes, 
    DesertMapNode startingNode,
    string directions
  )
  {
    Nodes = nodes;
    CurrentNode = startingNode;
    Directions = directions;
  }

  public void Navigate()
  {
    bool arrivedAtDestination = CurrentNode.IsDestinationNode();

    while(!arrivedAtDestination)
    {
      char direction = Directions[DirectionsIndex];
      
      if(direction == 'L')
      {
        CurrentNode = Nodes[CurrentNode.Left];
      }
      else
      {
        CurrentNode = Nodes[CurrentNode.Right];
      }

      arrivedAtDestination = CurrentNode.IsDestinationNode();
      Steps++;
      IncrementDirectionsIndex();
    }
  }

  private void IncrementDirectionsIndex()
  {
    DirectionsIndex++;
    DirectionsIndex %= Directions.Length;
  }
}