string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/18"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

int maxDistanceEast, minDistanceWest, minDistanceNorth, maxDistanceSouth;
maxDistanceEast = minDistanceWest = minDistanceNorth = maxDistanceSouth = 0;

int currentDistanceEW = 0;
int currentDistanceNS = 0;

foreach(string instruction in input)
{
  char direction = instruction[0];
  int distance = int.Parse(instruction.Split(' ')[1]);
 
  switch(direction) {
    case 'R' :
      currentDistanceEW += distance;
      if(currentDistanceEW > maxDistanceEast) maxDistanceEast = currentDistanceEW;
      break;
    case 'L' :
      currentDistanceEW -= distance;
      if(currentDistanceEW < minDistanceWest) minDistanceWest = currentDistanceEW;
      break;
    case 'D' :
      currentDistanceNS += distance;
      if(currentDistanceNS > maxDistanceSouth) maxDistanceSouth = currentDistanceNS;
      break;
    case 'U' :
      currentDistanceNS -= distance;
      if(currentDistanceNS < minDistanceNorth) minDistanceNorth = currentDistanceNS;
      break;
  }
}

int lavaductWidth = Math.Abs(minDistanceWest) + maxDistanceEast + 1;
int lavaductHeight = Math.Abs(minDistanceNorth) + maxDistanceSouth + 1;

char[,] lavaduct = new char[lavaductHeight, lavaductWidth];

int column = Math.Abs(minDistanceWest);
int row = Math.Abs(minDistanceNorth);

lavaduct[row, column] = '#';
int totalArea = 1;

foreach(string instruction in input)
{
  char direction = instruction[0];
  int distance = int.Parse(instruction.Split(' ')[1]);
 
  switch(direction) {
    case 'R' :  
      for(int i = 0; i < distance; i++)
      {
        column++;
        if(lavaduct[row,column] != '#')
        {
          lavaduct[row, column] = '#';
          totalArea++;
        }
      }
      break;
    case 'L' :
      for(int i = 0; i < distance; i++)
      {
        column--;
        if(lavaduct[row,column] != '#')
        {
          lavaduct[row, column] = '#';
          totalArea++;
        }
      }
      break;
    case 'D' :
      for(int i = 0; i < distance; i++)
      {
        row++;
        if(lavaduct[row,column] != '#')
        {
          lavaduct[row, column] = '#';
          totalArea++;
        }
      }
      break;
    case 'U' :
      for(int i = 0; i < distance; i++)
      {
        row--;
        if(lavaduct[row,column] != '#')
        {
          lavaduct[row, column] = '#';
          totalArea++;
        }
      }
      break;
  }
}

//flood fill
int centerColumn = lavaductWidth / 2;
int centerRow = lavaductHeight / 2;

FloodFill(centerRow, centerColumn);

void FloodFill(int row, int column)
{
  LinkedList<(int, int)> queue = new();
  queue.AddLast((row, column));

  while(queue.First != null)
  {
    var (r, c) = queue.First.Value;
    queue.RemoveFirst();

    if(lavaduct[r, c] != '#')
    {
      lavaduct[r, c] = '#';
      totalArea++;

      if(r - 1 >= 0) queue.AddLast((r - 1, c));
      if(r + 1 < lavaduct.GetLength(0)) queue.AddLast((r + 1, c));
      if(c - 1 >= 0) queue.AddLast((r, c - 1));
      if(c + 1 < lavaduct.GetLength(1)) queue.AddLast((r, c + 1));
    }
  }
}

Console.WriteLine(totalArea);

