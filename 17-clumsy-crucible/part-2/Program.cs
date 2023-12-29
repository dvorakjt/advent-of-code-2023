string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/17"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

CruciblePathGraph cruciblePathGraph = InputParser.CreateCruciblePathGraphFromInput(input);

string message = 
  cruciblePathGraph.LeastCostlyPath != -1 ? 
    $"The least costly path will incur a total heat loss of {cruciblePathGraph.LeastCostlyPath}" :
    "No path to the destination was found";

Console.WriteLine(message);