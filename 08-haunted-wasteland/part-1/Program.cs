string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in current directory.");
}

string input = File.ReadAllText(inputFilePath);

DesertMap map = InputParser.GetMap(input);
string directions = InputParser.GetDirections(input);

int totalSteps = map.Navigate("AAA", "ZZZ", directions);

Console.WriteLine($"The total number of steps to navigate from node 'AAA' to node 'ZZZ' was {totalSteps}");