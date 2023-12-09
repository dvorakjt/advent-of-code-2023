string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/3");
}

string[] input = File.ReadAllLines(inputFilePath);

GearFinder gearFinder = new GearFinder(input);

var gears = gearFinder.FindGears();

int gearSum = gears.Sum(partNumbers => partNumbers.Aggregate(1, (acc, partNumber) => acc * partNumber));

Console.WriteLine($"The sum of all gear ratios is {gearSum}");