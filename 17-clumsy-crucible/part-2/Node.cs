class Node(bool isDestination, int leastCostlyPath) : IComparable<Node>
{
  public readonly Dictionary<Node, int> EdgeWeights = [];
  public int LeastCostlyPath = leastCostlyPath;
  public bool IsDestination = isDestination;
  public bool Visited = false;
  
  public void AddEdgeToNode(Node adjacentNode, int edgeWeight)
  {
    EdgeWeights[adjacentNode] = edgeWeight;
  }

  public int CompareTo(Node? other)
  {
    ArgumentNullException.ThrowIfNull(other);

    return LeastCostlyPath - other.LeastCostlyPath;
  }
}