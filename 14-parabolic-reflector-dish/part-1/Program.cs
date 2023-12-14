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

TiltPlatformNorth(platform);
int load = CalculateLoad(platform);

Console.WriteLine($"The load on the north support beams is {load}");

void TiltPlatformNorth(char[,] platform)
{
  int[] buffers = new int[platform.GetLength(1)];

  for(int row = platform.GetLength(0) - 1; row >= 0; row--)
  {
    for(int column = 0; column < platform.GetLength(1); column++)
    {
      if(platform[row,column] == 'O')
      {
        platform[row,column] = '.';
        buffers[column]++;
      }
      else if(platform[row,column] == '#')
      {
        for(; buffers[column] > 0; buffers[column]--)
        {
          platform[row + buffers[column], column] = 'O';
        }
      }
    }
  }

  for(int column = 0; column < buffers.Length; column++)
  {
    int row = 0;

    while(buffers[column]-- > 0)
    {
      platform[row++, column] = 'O';
    }
  }
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