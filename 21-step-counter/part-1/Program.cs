string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/21"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

ReachablePlotFinder reachablePlotFinder = InputParser.CreateReachablePlotFinderFromInput(input, 64);

Console.WriteLine($"The number of tiles the elf can reach in exactly 64 steps is {reachablePlotFinder.ReachablePlotCount}");