string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/15"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

for(int i = 0; i < input.Length; i++)
{
  input[i] = input[i].Trim();
}

char[,] tiles = new char[input.Length, input[0].Length];

for(int i = 0; i < input.Length; i++)
{
  for(int j = 0; j < input[i].Length; j++)
  {
    tiles[i,j] = input[i][j];
  }
}

LaserGrid laserGrid = new(tiles);

Console.WriteLine($"The number of tiles energized by the laser beam is {laserGrid.EnergizedTiles}");