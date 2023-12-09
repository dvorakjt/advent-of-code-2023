using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/9");
}

string[] input = File.ReadAllLines(inputFilePath);

long extrapolatedSum = 0;

foreach(string line in input)
{
  MatchCollection matches = Regex.Matches(line, @"-{0,1}\d+");

  if(matches == null) continue;

  List<long> history = new(matches.Select(m => long.Parse(m.Value)));

  extrapolatedSum += ExtrapolateBackwards(history);
}

Console.WriteLine($"The sum of all extrapolated values is {extrapolatedSum}");

long ExtrapolateBackwards(List<long> values)
{
  if(values.All(v => v == 0))
  {
    return 0;
  }

  List<long> secondaryValues = new();

  for(int i = 1; i < values.Count; i++)
  {
    long secondaryValue = values[i] - values[i - 1];
    secondaryValues.Add(secondaryValue);
  }

  return values[0] - ExtrapolateBackwards(secondaryValues);
}