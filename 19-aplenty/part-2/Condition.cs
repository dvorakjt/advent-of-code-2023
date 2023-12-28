class Condition(string destinationWorkflowId, PartProperty partProperty, ComparisonOperator comparisonOperator, int comparand)
{
  private readonly PartProperty PartProperty = partProperty;
  private readonly ComparisonOperator ComparisonOperator = comparisonOperator;
  private readonly int Comparand = comparand;
  public readonly string DestinationWorkflowId = destinationWorkflowId;

  public (ImaginaryPart? ToDestinationWorkflow, ImaginaryPart? ToNextCondition) ProcessImaginaryPart(ImaginaryPart imaginaryPart)
  {
    if(ComparisonOperator == ComparisonOperator.LT)
    {
      return imaginaryPart.SplitAtMax(PartProperty, Comparand);
    }
    else
    {
      return imaginaryPart.SplitAtMin(PartProperty, Comparand);
    }
  }
}