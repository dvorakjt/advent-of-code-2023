string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/10");
}

string[] input = File.ReadAllLines(inputFilePath);

Maze maze = InputParser.CreateMazeFromInput(input);
List<Point> loopPath = maze.FindLoopPath();
LabeledMatrix labeledMatrix = new LabeledMatrix(maze.Height, maze.Width, loopPath);

Console.WriteLine($"The total area enclosed by the loop is: {labeledMatrix.InnerPointCount}");