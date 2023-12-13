
using System.Text.RegularExpressions;

string input = File.ReadAllText("input.txt");

MatchCollection matches = Regex.Matches(input, @"([#.]+\n)+");

long sum = 0;

foreach(Match match in matches)
{
  string[] split = match.Value.Split('\n', StringSplitOptions.RemoveEmptyEntries);
  for(int i = 0; i < split.Length; i++)
  {
    split[i] = split[i].Trim();
  }
  
  char[,] matrix = new char[split.Length, split[0].Length];

  for(int i = 0; i < split.Length; i++)
  {
    for(int j = 0; j < split[0].Length; j++)
    {
      matrix[i,j] = split[i][j];
    }
  }

  int columnsToLeft = 0;
  int columnsAbove = 0;

  if(TryFindVerticalLineOfSymmetryWithSmudge(matrix, out columnsToLeft))
  {
    sum += columnsToLeft;
  }
  if(TryFindHorizontalLineOfSymmetryWithSmudge(matrix, out columnsAbove))
  {
    sum += columnsAbove * 100;
  }
}

Console.WriteLine(sum);

bool TryFindVerticalLineOfSymmetryWithSmudge(char[,] matrix, out int columnsToLeft)
{
  int centerColumnLeft = 0;
  int centerColumnRight = 1;
  int c2L = 0;

  for( ; centerColumnRight < matrix.GetLength(1); centerColumnLeft++, centerColumnRight++)
  {
    int differencesDetected = 0;

    for(int left = centerColumnLeft, right = centerColumnRight; left >= 0 && right < matrix.GetLength(1); left--, right++)
    {
      for(int i = 0; i < matrix.GetLength(0); i++)
      {
        if(matrix[i,left] != matrix[i,right])
        {
          differencesDetected++;
        }
      }
    } 

    if(differencesDetected == 1) c2L = centerColumnLeft + 1;
  }

  columnsToLeft = c2L;

  return columnsToLeft > 0;
}

bool TryFindHorizontalLineOfSymmetryWithSmudge(char[,] matrix, out int columnsAbove)
{
  int centerRowTop = 0;
  int centerRowBottom = 1;
  int cAbove = 0;

  for( ; centerRowBottom < matrix.GetLength(0); centerRowTop++, centerRowBottom++)
  {
    int differencesDetected = 0;

    for(int top = centerRowTop, bottom = centerRowBottom; top >= 0 && bottom < matrix.GetLength(0); top--, bottom++)
    {
      for(int i = 0; i < matrix.GetLength(1); i++)
      {
        if(matrix[top,i] != matrix[bottom,i])
        {
          differencesDetected++;
        }
      }
    } 

    if(differencesDetected == 1) cAbove = centerRowTop + 1;
  }

  columnsAbove = cAbove;

  return columnsAbove > 0;
}