using System.Text.RegularExpressions;

static class InputParser
{
  private static readonly string WorkflowDetailsPattern = @"\w+{([xmas][><]\d+:\w+,)*\w+}";

  public static Dictionary<string, Workflow> CreateWorkflowsFromInput(string[] input)
  {
    List<string> workflowDetailsList = input
      .Select(line => line.Trim())
      .Where(line => Regex.IsMatch(line, WorkflowDetailsPattern))
      .ToList();

    Dictionary<string, Workflow> workflows = [];

    foreach(string workflowDetails in workflowDetailsList)
    {
      var (Key, Workflow) = ParseWorkflow(workflowDetails);
      workflows.Add(Key, Workflow);
    }

    return workflows;
  }
  private static (string Key, Workflow Workflow) ParseWorkflow(string workflowDetails)
  {
    int openingBraceIndex = workflowDetails.IndexOf("{");
    int closingBraceIndex = workflowDetails.IndexOf("}");

    string key = workflowDetails[..openingBraceIndex];
    string[] conditionDetailsList = workflowDetails[(openingBraceIndex + 1)..closingBraceIndex].Split(',');

    List<Condition> conditions = [];

    for(int i = 0; i < conditionDetailsList.Length - 1; i++)
    {
      string conditionDetails = conditionDetailsList[i];

      PartProperty partProperty;

      switch(conditionDetails[0])
      {
        case 'x':
          partProperty = PartProperty.X;
          break;
        case 'm':
          partProperty = PartProperty.M;
          break;
        case 'a':
          partProperty = PartProperty.A;
          break;
        case 's':
          partProperty = PartProperty.S;
          break;
        default:
          throw new ArgumentException("Character at index 0 of condition details string must be one of 'X', 'M', 'A', or 'S'");
      }
      ComparisonOperator comparisonOperator = conditionDetails[1] == '<' ? ComparisonOperator.LESS_THAN : ComparisonOperator.GREATER_THAN;
      int comparand = int.Parse(conditionDetails[2..conditionDetails.IndexOf(':')]);
      string destinationWorkflowId = conditionDetails[(conditionDetails.IndexOf(':') + 1)..];

      Condition condition = new(destinationWorkflowId, partProperty, comparisonOperator, comparand);
      conditions.Add(condition);
    }

    Workflow workflow = new(conditions, conditionDetailsList.Last());
    return (key, workflow);
  } 
}