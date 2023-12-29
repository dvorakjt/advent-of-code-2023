class CruciblePathGraph
{
  private const int MIN_BLOCKS_IN_ONE_DIRECTION = 4;
  private const int MAX_BLOCKS_IN_ONE_DIRECTION = 10;
  private readonly Dictionary<Point, (Node NSToEWNode, Node EWToNSNode)> Graph = [];
  private readonly int[,] HeatLossMap;
  private readonly Point Origin = new Point(0, 0);
  private readonly Point Destination;
  public int LeastCostlyPath { get; private set; }

  public CruciblePathGraph(int[,] heatLossMap)
  {
    HeatLossMap = new int[heatLossMap.GetLength(0), heatLossMap.GetLength(1)];
    Destination = new Point(HeatLossMap.GetLength(0) - 1, HeatLossMap.GetLength(1) - 1);

    for(int i = 0; i < heatLossMap.GetLength(0); i++)
    {
      for(int j = 0; j < heatLossMap.GetLength(1); j++)
      {
        HeatLossMap[i,j] = heatLossMap[i,j];
      }
    }

    InitializeNodesAndEdges();
    LeastCostlyPath = FindLeastCostlyPath();
  }

  private int FindLeastCostlyPath()
  {
    MinHeap<Node> unvisitedNodes = new();

    Node originNode = new Node(false, 0);

    originNode.AddEdgeToNode(Graph[Origin].NSToEWNode, 0);
    originNode.AddEdgeToNode(Graph[Origin].EWToNSNode, 0);

    unvisitedNodes.InsertOrUpdate(originNode);

    while(unvisitedNodes.Count > 0)
    {
      Node node = unvisitedNodes.ExtractMin();
      node.Visited = true;

      if(node.IsDestination)
      {
        return node.LeastCostlyPath;
      }

      foreach(var (adjacentNode, edgeWeight) in node.EdgeWeights)
      {
        if(!adjacentNode.Visited)
        {
          if(node.LeastCostlyPath + edgeWeight < adjacentNode.LeastCostlyPath)
          {
            adjacentNode.LeastCostlyPath = node.LeastCostlyPath + edgeWeight;
          }
          unvisitedNodes.InsertOrUpdate(adjacentNode);
        }
      }
    }

    return -1;
  }

  private void InitializeNodesAndEdges()
  {
    for(int i = 0; i < HeatLossMap.GetLength(0); i++)
    {
      for(int j = 0; j < HeatLossMap.GetLength(1); j++)
      {
        Point p = new Point(i, j);

        if(!Graph.ContainsKey(p))
        { 
          Graph[p] = 
          (
            new(IsDestination(p), int.MaxValue), 
            new(IsDestination(p), int.MaxValue)
          );
        }

        AddNorthernEdges(p);
        AddSouthernEdges(p);
        AddWesternEdges(p);
        AddEasternEdges(p);
      }
    }
  }

  private void AddNorthernEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int heatLoss = 0;

    for(int i = 1; i <= MAX_BLOCKS_IN_ONE_DIRECTION; i++)
    {
      int rowToNorth = p.Row - i;
      if(rowToNorth < 0) break;

      Point adjacentPoint = new(rowToNorth, p.Column);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

      if(i < MIN_BLOCKS_IN_ONE_DIRECTION) continue;

      if(!Graph.ContainsKey(adjacentPoint))
      { 
        Graph[adjacentPoint] = 
        (
          new(IsDestination(adjacentPoint), int.MaxValue), 
          new(IsDestination(adjacentPoint), int.MaxValue)
        );
      }

      var adjacentNodes = Graph[adjacentPoint];

      ewToNSNode.AddEdgeToNode(adjacentNodes.NSToEWNode, heatLoss);
    }
  }

  private void AddSouthernEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int heatLoss = 0;

    for(int i = 1; i <= MAX_BLOCKS_IN_ONE_DIRECTION; i++)
    {
      int rowToSouth = p.Row + i;
      if(rowToSouth >= HeatLossMap.GetLength(0)) break;

      Point adjacentPoint = new(rowToSouth, p.Column);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

      if(i < MIN_BLOCKS_IN_ONE_DIRECTION) continue;

      if(!Graph.ContainsKey(adjacentPoint))
      { 
        Graph[adjacentPoint] = 
        (
          new(IsDestination(adjacentPoint), int.MaxValue), 
          new(IsDestination(adjacentPoint), int.MaxValue)
        );
      }

      var adjacentNodes = Graph[adjacentPoint];

      ewToNSNode.AddEdgeToNode(adjacentNodes.NSToEWNode, heatLoss);
    }
  }

  private void AddWesternEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int heatLoss = 0;

    for(int i = 1; i <= MAX_BLOCKS_IN_ONE_DIRECTION; i++)
    {
      int colToWest = p.Column - i;
      if(colToWest < 0) break;

      Point adjacentPoint = new(p.Row, colToWest);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

      if(i < MIN_BLOCKS_IN_ONE_DIRECTION) continue;

      if(!Graph.ContainsKey(adjacentPoint))
      { 
        Graph[adjacentPoint] = 
        (
          new(IsDestination(adjacentPoint), int.MaxValue), 
          new(IsDestination(adjacentPoint), int.MaxValue)
        );
      }

      var adjacentNodes = Graph[adjacentPoint];

      nsToEWNode.AddEdgeToNode(adjacentNodes.EWToNSNode, heatLoss);
    }
  }

  private void AddEasternEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int heatLoss = 0;

    for(int i = 1; i <= MAX_BLOCKS_IN_ONE_DIRECTION; i++)
    {
      int colToEast = p.Column + i;
      if(colToEast >= HeatLossMap.GetLength(1)) break;

      Point adjacentPoint = new(p.Row, colToEast);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

      if(i < MIN_BLOCKS_IN_ONE_DIRECTION) continue;

      if(!Graph.ContainsKey(adjacentPoint))
      { 
        Graph[adjacentPoint] = 
        (
          new(IsDestination(adjacentPoint), int.MaxValue), 
          new(IsDestination(adjacentPoint), int.MaxValue)
        );
      }

      var adjacentNodes = Graph[adjacentPoint];

      nsToEWNode.AddEdgeToNode(adjacentNodes.EWToNSNode, heatLoss);
    }
  }

  bool IsDestination(Point p)
  {
    return p.Equals(Destination);
  }
}