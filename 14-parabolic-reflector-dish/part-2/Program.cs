string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/14"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

Platform platform = InputParser.ParseInput(input);

Console.WriteLine($"The load on the northern support beams after 1 billion cycles of tilting NWSE is {platform.CalculateLoadAfter1BillionCycles()}");

