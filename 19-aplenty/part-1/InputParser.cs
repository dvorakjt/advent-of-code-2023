using System.Text.RegularExpressions;

class InputParser
{
  private readonly List<string> WorkflowDetailsList = new();
  private readonly List<string> PartDetailsList = new();
  private readonly string WorkflowDetailsPattern = @"\w+{([xmas][><]\d+:\w+,)*\w+}";
  private readonly string PartDetailsPattern = @"{x=\d+,m=\d+,a=\d+,s=\d+}";

  public InputParser(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      string detail = input[i].Trim();

      if(Regex.IsMatch(detail, WorkflowDetailsPattern))
      {
        WorkflowDetailsList.Add(detail);
      }
      else if(Regex.IsMatch(detail, PartDetailsPattern))
      {
        PartDetailsList.Add(detail);
      }
    }
  }

  public Dictionary<string, Workflow> ParseWorkflows()
  {
    Dictionary<string, Workflow> workflows = new();

    foreach(string workflowDetails in WorkflowDetailsList)
    {
      var (Key, Workflow) = ParseWorkflow(workflowDetails);
      workflows.Add(Key, Workflow);
    }

    return workflows;
  }
  private (string Key, Workflow Workflow) ParseWorkflow(string workflowDetails)
  {
    int openingBraceIndex = workflowDetails.IndexOf("{");
    int closingBraceIndex = workflowDetails.IndexOf("}");

    string key = workflowDetails[..openingBraceIndex];
    string[] conditionDetailsList = workflowDetails[(openingBraceIndex + 1)..closingBraceIndex].Split(',');

    List<Condition> conditions = new();

    for(int i = 0; i < conditionDetailsList.Length - 1; i++)
    {
      string conditionDetails = conditionDetailsList[i];

      char partProperty = conditionDetails[0];
      char comparisonOperator = conditionDetails[1];
      int benchmark = int.Parse(conditionDetails[2..conditionDetails.IndexOf(':')]);

      Predicate<Part> meetsCondition = CreateMeetsConditionFunc(partProperty, comparisonOperator, benchmark);
      string destination = conditionDetails[(conditionDetails.IndexOf(':') + 1)..];

      Condition condition = new(destination, meetsCondition);
      conditions.Add(condition);
    }

    Workflow workflow = new(conditions, conditionDetailsList.Last());
    return (key, workflow);
  } 

  private Predicate<Part> CreateMeetsConditionFunc(char partProperty, char comparisonOperator, int benchmark)
  {
    return (Part p) => {
      int propertyValue;
      
      switch(partProperty)
      {
        case 'x' :
          propertyValue = p.X;
          break;
        case 'm' :
          propertyValue = p.M;
          break;
        case 'a' :
          propertyValue = p.A;
          break;
        default :
          propertyValue = p.S;
          break;
      }

      if(comparisonOperator == '>') return propertyValue > benchmark;

      return propertyValue < benchmark;
    };
  }

  public List<Part> ParseParts()
  {
    return PartDetailsList.Select(ParsePart).ToList();
  }

  private Part ParsePart(string partDetails)
  {
    MatchCollection categoryValueMatches = Regex.Matches(partDetails, @"\d+");
    List<int> categoryValues = categoryValueMatches.Select(match => int.Parse(match.Value)).ToList();
    return new Part(categoryValues[0], categoryValues[1], categoryValues[2], categoryValues[3]);
  }
}