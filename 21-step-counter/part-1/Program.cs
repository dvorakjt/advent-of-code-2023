string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/21"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

// for(int i = 0; i <= 6; i++)
// {

ReachablePlotFinder reachablePlotFinder = InputParser.CreateReachablePlotFinderFromInput(input, 64);

Console.WriteLine(reachablePlotFinder.ReachablePlotCount);

reachablePlotFinder.SaveVisualization("64");

