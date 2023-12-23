string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/18"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

Lavaduct lavaduct = InputParser.ParseInput(input);
double area = lavaduct.LocateOuterCornersAndCalculateArea();

Console.WriteLine($"The total excavated area is {area}");

