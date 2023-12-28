class Workflow(List<Condition> conditions, string defaultDestinationWorkflowId)
{
  public const string ENTRY_POINT_WORKFLOW_ID = "in";
  private const string ACCEPTED_PARTS_ID = "A";
  private const string REJECTED_PARTS_ID = "R";
  private readonly List<Condition> Conditions = conditions;
  private readonly string DefaultDestinationWorkflowId = defaultDestinationWorkflowId;

  public void ProcessImaginaryPart
  (
    ImaginaryPart imaginaryPart, 
    Dictionary<string, Workflow> destinationWorkflows, 
    List<ImaginaryPart> acceptedParts
  )
  {
    ImaginaryPart? toNextCondition = imaginaryPart;

    foreach(var condition in Conditions)
    {
      var (ToDestinationWorkflow, ToNextCondition) = condition.ProcessImaginaryPart(toNextCondition);

      if(ToDestinationWorkflow != null)
      {
        if(condition.DestinationWorkflowId == ACCEPTED_PARTS_ID)
        {
          acceptedParts.Add(ToDestinationWorkflow);
        }
        else if(destinationWorkflows.TryGetValue(condition.DestinationWorkflowId, out Workflow? destinationWorkflow))
        {
          destinationWorkflow.ProcessImaginaryPart(ToDestinationWorkflow, destinationWorkflows, acceptedParts);
        }
      }
      
      toNextCondition = ToNextCondition;
      if(toNextCondition == null) break;
    }

    if(toNextCondition != null)
    {
      if(DefaultDestinationWorkflowId == ACCEPTED_PARTS_ID)
      {
        acceptedParts.Add(toNextCondition);
      }
      else if(DefaultDestinationWorkflowId != REJECTED_PARTS_ID)
      {
        destinationWorkflows[DefaultDestinationWorkflowId].ProcessImaginaryPart(toNextCondition, destinationWorkflows, acceptedParts);
      } 
    }
  }
}