string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/10");
}

string[] input = File.ReadAllLines(inputFilePath);

Maze maze = InputParser.CreateMazeFromInput(input);
maze.FindLoop();
LabeledMatrix labeledMatrix = new LabeledMatrix(input.Length, input[0].Length, maze.LoopPath, input);

Console.WriteLine(labeledMatrix.InnerPointCount);

labeledMatrix.Save();