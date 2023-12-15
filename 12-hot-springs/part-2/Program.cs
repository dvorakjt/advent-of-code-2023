string[] input = File.ReadAllLines("input.txt");

var incompleteConditionRecords = InputParser.GetIncompleteConditionRecordsFromInput(input);

long possibleGroupArrangementsSum = 
  incompleteConditionRecords
    .Select(record => record.PossibleGroupArrangementsCount)
    .Sum();

Console.WriteLine(
  $"The sum of all possible arrangements of damaged spring groups after unfolding the condition record is {possibleGroupArrangementsSum}"
);