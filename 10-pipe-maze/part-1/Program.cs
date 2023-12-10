string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/10");
}

string[] input = File.ReadAllLines(inputFilePath);

Maze maze = InputParser.CreateMazeFromInput(input);
long furthestPoint = maze.FindFurthestPoint();

Console.WriteLine(furthestPoint);