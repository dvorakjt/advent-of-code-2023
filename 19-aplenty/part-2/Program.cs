string[] input = File.ReadAllLines("input.txt");

var workflows = InputParser.CreateWorkflowsFromInput(input);

List<ImaginaryPart> acceptedParts = [];

ImaginaryPart imaginaryPart = new();

workflows[Workflow.ENTRY_POINT_WORKFLOW_ID].ProcessImaginaryPart(imaginaryPart, workflows, acceptedParts);

long totalDistinctCombinationsOfAcceptedRatings = 0;

foreach(var acceptedPart in acceptedParts)
{
  totalDistinctCombinationsOfAcceptedRatings += acceptedPart.CountPossibleCombinations();
}

Console.WriteLine($"The total distinct combinations of ratings that will be accepted by the elves' workflows is: {totalDistinctCombinationsOfAcceptedRatings}");