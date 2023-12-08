class DesertMap
{
  private Dictionary<string, DesertMapNode> Nodes = new();
  private List<DesertMapNode> StartingNodes = new();

  public void AddNode(string identifier, string left, string right)
  {
    DesertMapNode n = new DesertMapNode(identifier, left, right);
    Nodes.Add(n.Identifier, n);

    if(n.IsStartingNode())
    {
      StartingNodes.Add(n);
    }
  }

  public async Task<long> Navigate(string directions)
  {
    List<Navigator> navigators = new List<Navigator>();

    foreach(var node in StartingNodes)
    {
      Navigator navigator = new(Nodes, node, directions);
      navigators.Add(navigator);
    }

    List<Task> tasks = new List<Task>();

    foreach(var navigator in navigators)
    {
      tasks.Add(Task.Run(() => navigator.Navigate()));
    }

    await Task.WhenAll(tasks);

    long totalSteps = LeastCommonMultiple.LCM(navigators.Select(n => n.Steps));

    return totalSteps;
  }
}