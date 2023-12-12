string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/12");
}

string[] input = File.ReadAllLines(inputFilePath);

List<IncompleteConditionRecord> incompleteConditionRecords = InputParser.GetIncompleteConditionRecordsFromInput(input);

long possibleGroupArrangementsSum = incompleteConditionRecords.Sum(r => r.PossibleGroupArrangementsCount);

Console.WriteLine($"The sum of all possible arrangements of broken and operational springs is {possibleGroupArrangementsSum}");