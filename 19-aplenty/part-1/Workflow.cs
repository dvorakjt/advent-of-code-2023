class Workflow
{
  private List<Condition> Conditions;
  private string DefaultDestination;

  public Workflow(List<Condition> conditions, string defaultDestination)
  {
    Conditions = new(conditions);
    DefaultDestination = defaultDestination;
  }

  public string ProcessPart(Part part)
  {
    foreach(Condition condition in Conditions)
    {
      if(condition.MeetsCondition(part))
      {
        return condition.Destination;
      }
    }

    return DefaultDestination;
  }
}