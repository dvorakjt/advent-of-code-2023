string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/22"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

BrickPile brickPile = InputParser.ParseInput(input);

Console.WriteLine($"The total number of bricks that can be disintegrated by chain reactions is {brickPile.ChainReactionDisintegratedBricksSum}");