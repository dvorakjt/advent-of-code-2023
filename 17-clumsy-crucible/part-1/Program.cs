string[] input = File.ReadAllLines("input.txt");

for(int i = 0; i < input.Length; i++)
{
  input[i] = input[i].Trim();
}

int[,] heatLossMatrix = new int[input.Length, input[0].Length];

for(int i = 0; i < input.Length; i++)
{
  for(int j = 0; j < input[0].Length; j++)
  {
    heatLossMatrix[i,j] = input[i][j] - '0';
  }
}

Point origin = new Point(0, 0);
Point destination = new Point(heatLossMatrix.GetLength(0) - 1, heatLossMatrix.GetLength(1) - 1);

Dictionary<Point, (Node NSToEWNode, Node EWToNSNode)> graph = [];

for(int i = 0; i < heatLossMatrix.GetLength(0); i++)
{
  for(int j = 0; j < heatLossMatrix.GetLength(1); j++)
  {
    Point p = new Point(i, j);

    if(!graph.ContainsKey(p))
    { 
      graph[p] = 
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

MinHeap<Node> unvisitedNodes = new();

Node originNode = new Node(false, 0);

originNode.AddEdgeToNode(graph[origin].NSToEWNode, 0);
originNode.AddEdgeToNode(graph[origin].EWToNSNode, 0);

unvisitedNodes.InsertOrUpdate(originNode);

int lcp = int.MaxValue;

while(unvisitedNodes.Count > 0)
{
  Node node = unvisitedNodes.ExtractMin();
  node.Visited = true;

  if(node.IsDestination)
  {
    lcp = node.LeastCostlyPath;
    break;
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

Console.WriteLine(lcp);

void AddNorthernEdges(Point p)
{
  var (nsToEWNode, ewToNSNode) = graph[p];

  int rowsMovedToNorth = 1;
  int rowToNorth = p.Row - rowsMovedToNorth;
  int heatLoss = 0;
  
  while(rowToNorth >= 0 && rowsMovedToNorth <= 3)
  {
    Point adjacentPoint = new(rowToNorth, p.Column);
    heatLoss += heatLossMatrix[adjacentPoint.Row, adjacentPoint.Column];

    if(!graph.ContainsKey(adjacentPoint))
    { 
      graph[adjacentPoint] = 
      (
        new(IsDestination(adjacentPoint), int.MaxValue), 
        new(IsDestination(adjacentPoint), int.MaxValue)
      );
    }

    var adjacentNodes = graph[adjacentPoint];

    ewToNSNode.AddEdgeToNode(adjacentNodes.NSToEWNode, heatLoss);

    rowsMovedToNorth++;
    rowToNorth--;
  }
}

void AddSouthernEdges(Point p)
{
  var (nsToEWNode, ewToNSNode) = graph[p];

  int rowsMovedToSouth = 1;
  int rowToSouth = p.Row + rowsMovedToSouth;
  int heatLoss = 0;
  
  while(rowToSouth < heatLossMatrix.GetLength(0) && rowsMovedToSouth <= 3)
  {
    Point adjacentPoint = new(rowToSouth, p.Column);
    heatLoss += heatLossMatrix[adjacentPoint.Row, adjacentPoint.Column];

    if(!graph.ContainsKey(adjacentPoint))
    { 
      graph[adjacentPoint] = 
      (
        new(IsDestination(adjacentPoint), int.MaxValue), 
        new(IsDestination(adjacentPoint), int.MaxValue)
      );
    }

    var adjacentNodes = graph[adjacentPoint];

    ewToNSNode.AddEdgeToNode(adjacentNodes.NSToEWNode, heatLoss);

    rowsMovedToSouth++;
    rowToSouth++;
  }
}

void AddWesternEdges(Point p)
{
  var (nsToEWNode, ewToNSNode) = graph[p];

  int colsMovedToWest = 1;
  int colWest = p.Column - colsMovedToWest;
  int heatLoss = 0;
  
  while(colWest >= 0 && colsMovedToWest <= 3)
  {
    Point adjacentPoint = new(p.Row, colWest);
    heatLoss += heatLossMatrix[adjacentPoint.Row, adjacentPoint.Column];

    if(!graph.ContainsKey(adjacentPoint))
    { 
      graph[adjacentPoint] = 
      (
        new(IsDestination(adjacentPoint), int.MaxValue), 
        new(IsDestination(adjacentPoint), int.MaxValue)
      );;
    }

    var adjacentNodes = graph[adjacentPoint];

    nsToEWNode.AddEdgeToNode(adjacentNodes.EWToNSNode, heatLoss);

    colsMovedToWest++;
    colWest--;
  }
}

void AddEasternEdges(Point p)
{
  var (nsToEWNode, ewToNSNode) = graph[p];

  int colsMovedToEast = 1;
  int colEast = p.Column + colsMovedToEast;
  int heatLoss = 0;
  
 while(colEast < heatLossMatrix.GetLength(1) && colsMovedToEast <= 3)
  {
    Point adjacentPoint = new(p.Row, colEast);
    heatLoss += heatLossMatrix[adjacentPoint.Row, adjacentPoint.Column];

    if(!graph.ContainsKey(adjacentPoint))
    { 
      graph[adjacentPoint] = 
      (
        new(IsDestination(adjacentPoint), int.MaxValue), 
        new(IsDestination(adjacentPoint), int.MaxValue)
      );
    }

    var adjacentNodes = graph[adjacentPoint];

    nsToEWNode.AddEdgeToNode(adjacentNodes.EWToNSNode, heatLoss);

    colsMovedToEast++;
    colEast++;
  }
}

bool IsDestination(Point p)
{
  return p.Equals(destination);
}