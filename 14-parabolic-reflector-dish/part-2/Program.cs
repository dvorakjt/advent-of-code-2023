string[] input = File.ReadAllLines("input.txt");

for(int i = 0; i < input.Length; i++)
{
  input[i] = input[i].Trim();
}

char[,] platform = new char[input.Length, input[0].Length];

for(int i = 0; i < platform.GetLength(0); i++)
{
  for(int j = 0; j < platform.GetLength(1); j++)
  {
    platform[i,j] = input[i][j];
  }
}

for(int i = 0; i < 1e9; i++)
{
  platform = PerformCycle(platform);
  Console.Write(i);
  Console.SetCursorPosition(0, Console.CursorTop);
}

Console.WriteLine(CalculateLoad(platform));

char[,] PerformCycle(char[,] previousCycle)
{
  char[,] tiltedNorth = TiltPlatformNorth(previousCycle);
  char[,] tiltedWest = TiltPlatformWest(tiltedNorth);
  char[,] tiltedSouth = TiltPlatformSouth(tiltedWest);
  char[,] tiltedEast = TiltPlatformEast(tiltedSouth);

  return tiltedEast;
}

char[,] TiltPlatformNorth(char[,] platform)
{
  char[,] tiltedPlatform = new char[platform.GetLength(0), platform.GetLength(1)];
  int[] buffers = new int[platform.GetLength(1)];

  for(int row = platform.GetLength(0) - 1; row >= 0; row--)
  {
    for(int column = 0; column < platform.GetLength(1); column++)
    {
      switch(platform[row,column])
      {
        case '.' :
          tiltedPlatform[row,column] = '.';
          break;
        case 'O' :
          tiltedPlatform[row, column] = '.';
          buffers[column]++;
          break;
        case '#' :
          tiltedPlatform[row, column] = '#';
          for(; buffers[column] > 0; buffers[column]--)
          {
            tiltedPlatform[row + buffers[column], column] = 'O';
          }
          break;
      }
    }
  }

  for(int column = 0; column < buffers.Length; column++)
  {
    int row = 0;

    while(buffers[column]-- > 0)
    {
      tiltedPlatform[row++, column] = 'O';
    }
  }

  return tiltedPlatform;
}
char[,] TiltPlatformWest(char[,] platform)
{
  char[,] tiltedPlatform = new char[platform.GetLength(0), platform.GetLength(1)];
  int[] buffers = new int[platform.GetLength(0)];

  for(int column = platform.GetLength(1) - 1; column >= 0; column--)
  {
    for(int row = 0; row < platform.GetLength(0); row++)
    {
      switch(platform[row,column])
      {
        case '.' :
          tiltedPlatform[row,column] = '.';
          break;
        case 'O' :
          tiltedPlatform[row, column] = '.';
          buffers[row]++;
          break;
        case '#' :
          tiltedPlatform[row, column] = '#';
          for(; buffers[row] > 0; buffers[row]--)
          {
            tiltedPlatform[row, column + buffers[row]] = 'O';
          }
          break;
      }
    }
  }

  for(int row = 0; row < buffers.Length; row++)
  {
    int column = 0;

    while(buffers[row]-- > 0)
    {
      tiltedPlatform[row, column++] = 'O';
    }
  }

  return tiltedPlatform;
}
char[,] TiltPlatformSouth(char[,] platform)
{
  char[,] tiltedPlatform = new char[platform.GetLength(0), platform.GetLength(1)];
  int[] buffers = new int[platform.GetLength(1)];

  for(int row = 0; row < platform.GetLength(0); row++)
  {
    for(int column = 0; column < platform.GetLength(1); column++)
    {
      switch(platform[row,column])
      {
        case '.' :
          tiltedPlatform[row,column] = '.';
          break;
        case 'O' :
          tiltedPlatform[row, column] = '.';
          buffers[column]++;
          break;
        case '#' :
          tiltedPlatform[row, column] = '#';
          for(; buffers[column] > 0; buffers[column]--)
          {
            tiltedPlatform[row - buffers[column], column] = 'O';
          }
          break;
      }
    }
  }

  for(int column = 0; column < buffers.Length; column++)
  {
    int row = platform.GetLength(0) - 1;

    while(buffers[column]-- > 0)
    {
      tiltedPlatform[row--, column] = 'O';
    }
  }

  return tiltedPlatform;
}
char[,] TiltPlatformEast(char[,] platform)
{
  char[,] tiltedPlatform = new char[platform.GetLength(0), platform.GetLength(1)];
  int[] buffers = new int[platform.GetLength(0)];

  for(int column = 0; column < platform.GetLength(1); column++)
  {
    for(int row = 0; row < platform.GetLength(0); row++)
    {
      switch(platform[row,column])
      {
        case '.' :
          tiltedPlatform[row,column] = '.';
          break;
        case 'O' :
          tiltedPlatform[row, column] = '.';
          buffers[row]++;
          break;
        case '#' :
          tiltedPlatform[row, column] = '#';
          for(; buffers[row] > 0; buffers[row]--)
          {
            tiltedPlatform[row, column - buffers[row]] = 'O';
          }
          break;
      }
    }
  }

  for(int row = 0; row < buffers.Length; row++)
  {
    int column = platform.GetLength(1) - 1;

    while(buffers[row]-- > 0)
    {
      tiltedPlatform[row, column] = 'O';

      column--;
    }
  }

  return tiltedPlatform;
}


int CalculateLoad(char[,] platform)
{
  int load = 0;

  for(int row = 0; row < platform.GetLength(0); row++)
  {
    for(int column = 0; column < platform.GetLength(1); column++)
    {
      if(platform[row, column] == 'O')
      {
        load += platform.GetLength(0) - row;
      }
    }
  }

  return load;
}