string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/14"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

Matrix matrix = InputParser.ParseInput(input);

int cycles = 0;

while(true)
{
  matrix.PerformCycle();
  cycles++;
  if(matrix.CheckForCycleAndAddHash(cycles)) break;
}

string currentHash = MatrixHashGenerator.HashMatrix(matrix.Columns);

for(int i = 0; i < 36 + 27; i++)
{
  matrix.PerformCycle();
}

Console.WriteLine(matrix.CalculateLoad());

