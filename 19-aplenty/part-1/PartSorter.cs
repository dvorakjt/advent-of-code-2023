class PartSorter
{
  private const string STARTING_WORKFLOW_KEY = "in";
  private Dictionary<string, Workflow> Workflows;
  private List<Part> Parts;
  private List<Part> AcceptedParts = new();

  public int AcceptedPartsRatingSum 
  { 
    get 
    {
      return AcceptedParts.Sum(part => part.X + part.M + part.A + part.S);
    }
  }
  
  public PartSorter(Dictionary<string, Workflow> workflows, List<Part> parts)
  {
    Workflows = new(workflows);
    Parts = new(parts);
    SortParts();
  }

  private void SortParts()
  {
    foreach(Part part in Parts)
    {
      string workflowKey = STARTING_WORKFLOW_KEY;

      while(workflowKey != "A" && workflowKey != "R")
      {
        workflowKey = Workflows[workflowKey].ProcessPart(part);
        if(workflowKey == "A") AcceptedParts.Add(part);
      }
    }
  }
}