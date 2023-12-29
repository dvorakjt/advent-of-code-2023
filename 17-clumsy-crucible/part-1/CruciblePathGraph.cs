class CruciblePathGraph
{
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

    int rowsMovedToNorth = 1;
    int rowToNorth = p.Row - rowsMovedToNorth;
    int heatLoss = 0;
    
    while(rowToNorth >= 0 && rowsMovedToNorth <= 3)
    {
      Point adjacentPoint = new(rowToNorth, p.Column);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

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

      rowsMovedToNorth++;
      rowToNorth--;
    }
  }

  private void AddSouthernEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int rowsMovedToSouth = 1;
    int rowToSouth = p.Row + rowsMovedToSouth;
    int heatLoss = 0;
    
    while(rowToSouth < HeatLossMap.GetLength(0) && rowsMovedToSouth <= 3)
    {
      Point adjacentPoint = new(rowToSouth, p.Column);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

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

      rowsMovedToSouth++;
      rowToSouth++;
    }
  }

  private void AddWesternEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int colsMovedToWest = 1;
    int colWest = p.Column - colsMovedToWest;
    int heatLoss = 0;
    
    while(colWest >= 0 && colsMovedToWest <= 3)
    {
      Point adjacentPoint = new(p.Row, colWest);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

      if(!Graph.ContainsKey(adjacentPoint))
      { 
        Graph[adjacentPoint] = 
        (
          new(IsDestination(adjacentPoint), int.MaxValue), 
          new(IsDestination(adjacentPoint), int.MaxValue)
        );;
      }

      var adjacentNodes = Graph[adjacentPoint];

      nsToEWNode.AddEdgeToNode(adjacentNodes.EWToNSNode, heatLoss);

      colsMovedToWest++;
      colWest--;
    }
  }

  private void AddEasternEdges(Point p)
  {
    var (nsToEWNode, ewToNSNode) = Graph[p];

    int colsMovedToEast = 1;
    int colEast = p.Column + colsMovedToEast;
    int heatLoss = 0;
    
    while(colEast < HeatLossMap.GetLength(1) && colsMovedToEast <= 3)
    {
      Point adjacentPoint = new(p.Row, colEast);
      heatLoss += HeatLossMap[adjacentPoint.Row, adjacentPoint.Column];

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

      colsMovedToEast++;
      colEast++;
    }
  }

  bool IsDestination(Point p)
  {
    return p.Equals(Destination);
  }
}