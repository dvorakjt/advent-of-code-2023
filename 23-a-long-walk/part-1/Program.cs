string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/23"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

Console.WriteLine("Loaded map. Beginning search for longest path...");

PathFinder pathFinder = InputParser.CreatePathFinderFromInput(input);

Console.WriteLine($"The longest path through the forest without revisiting any tiles is {pathFinder.LongestPath}");