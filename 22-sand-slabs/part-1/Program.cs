string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/19"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

List<Brick> bricks = InputParser.GetSortedBricksFromInput(input);

int matrixWidth = bricks.Max(brick => brick.X2) - bricks.Min(brick => brick.X1) + 1;
int matrixDepth = bricks.Max(brick => brick.Y2) - bricks.Min(brick => brick.Y1) + 1;
int matrixHeight = bricks.Sum(brick => brick.Height);

ThreeDMatrix threeDMatrix = new(matrixWidth, matrixDepth, matrixHeight);

Dictionary<int, HashSet<int>> SupportedBy = new();

int i = 0;

while(i < bricks.Count)
{
  int currentElevation = bricks[i].Z1;

  while(i < bricks.Count)
  {
    Brick brick = bricks[i];
    if(brick.Z1 > currentElevation) break;

    int lowestLevel = 0;

    for(int z = currentElevation; z >= 0; z--)
    {
      for(int x = brick.X1; x <= brick.X2; x++)
      { 
        for(int y = brick.Y1; y <= brick.Y2; y++)
        {
          if(threeDMatrix.Matrix[x,y,z] > 0)
          {
            lowestLevel = z + 1;
            if(!SupportedBy.ContainsKey(brick.Id))
            {
              SupportedBy.Add(brick.Id, []);
            }
            SupportedBy[brick.Id].Add(threeDMatrix.Matrix[x,y,z]);
          }
        }
      }
      if(lowestLevel > 0) break;
    }

    for(int z = lowestLevel; z < lowestLevel + brick.Height; z++)
    {
      for(int x = brick.X1; x <= brick.X2; x++)
      { 
        for(int y = brick.Y1; y <= brick.Y2; y++)
        {
          threeDMatrix.Matrix[x,y,z] = brick.Id;
        }
      }
    }

    i++;
  }
}

int disintegratableBrickCount = bricks.Count;

foreach(var brick in bricks)
{
  foreach(var supportBricks in SupportedBy.Values)
  {
    if(supportBricks.Count == 1 && supportBricks.Contains(brick.Id))
    {
      disintegratableBrickCount--;
      break;
    }
  }
}

Console.WriteLine(disintegratableBrickCount);