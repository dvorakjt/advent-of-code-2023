class Workflow(List<Condition> conditions, string defaultDestinationWorkflowId)
{
  private Dictionary<string, Workflow> DestinationWorkflows = [];
  private List<Condition> Conditions = conditions;

  private string DefaultDestinationWorkflowId = defaultDestinationWorkflowId;

  public void AddConnectedWorkflow(string workflowId, Workflow workflow)
  {
    DestinationWorkflows.Add(workflowId, workflow);
  }

  public void ProcessImaginaryPart(ImaginaryPart imaginaryPart, List<ImaginaryPart> acceptedParts)
  {
    ImaginaryPart? unprocessedImaginaryPart = imaginaryPart;

    foreach(var condition in Conditions)
    {
      var (ToDestinationWorkflow, ToNextCondition) = condition.ProcessImaginaryPart(unprocessedImaginaryPart);

      if(ToDestinationWorkflow != null)
      {
        if(condition.DestinationWorkflowId == "A")
        {
          acceptedParts.Add(ToDestinationWorkflow);
        }
        else if(DestinationWorkflows.ContainsKey(condition.DestinationWorkflowId))
        {
          DestinationWorkflows[condition.DestinationWorkflowId].ProcessImaginaryPart(ToDestinationWorkflow, acceptedParts);
        }
      }
      
      unprocessedImaginaryPart = ToNextCondition;
      if(unprocessedImaginaryPart == null) break;
    }

    if(unprocessedImaginaryPart != null)
    {
      DestinationWorkflows[DefaultDestinationWorkflowId].ProcessImaginaryPart(unprocessedImaginaryPart, acceptedParts);
    }
  }

}