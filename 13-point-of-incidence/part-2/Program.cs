using System.Text.RegularExpressions;

string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/13"
  );
}

string input = File.ReadAllText(inputFilePath);
MatchCollection matches = Regex.Matches(input, @"([#.]+\n)+");
int sum = 0;

foreach(Match match in matches)
{
  char [,] matrix = GetMatrixFromMatch(match);

  sum += FindColumnsLeftOfLineOfReflectionWithDefect(matrix);
  sum += FindColumnsAboveLineOfReflectionWithDefect(matrix) * 100;
}

Console.WriteLine
(
  "With exactly one defect allowed:\n" +
  "The sum of all columns above vertical lines of reflection + \n" + 
  $"The sum of all rows above horizontal lines of reflection * 100 =\n{sum}"
);

char[,] GetMatrixFromMatch(Match match)
{
  string[] rows = match.Value.Split('\n', StringSplitOptions.RemoveEmptyEntries);
  for(int i = 0; i < rows.Length; i++)
  {
    rows[i] = rows[i].Trim();
  }
  
  char[,] matrix = new char[rows.Length, rows[0].Length];

  for(int i = 0; i < rows.Length; i++)
  {
    for(int j = 0; j < rows[0].Length; j++)
    {
      matrix[i,j] = rows[i][j];
    }
  } 

  return matrix;
}

int FindColumnsLeftOfLineOfReflectionWithDefect(char[,] matrix)
{
  (int LeftColumn, int RightColumn) lineOfReflection = (0, 1);
  int columnsToLeft = 0;

  for( ; lineOfReflection.RightColumn < matrix.GetLength(1); lineOfReflection.LeftColumn++, lineOfReflection.RightColumn++)
  {
    int defects = 0;

    for
    (
      int leftmostColumn = lineOfReflection.LeftColumn, rightmostColumn = lineOfReflection.RightColumn; 
      leftmostColumn >= 0 && rightmostColumn < matrix.GetLength(1); 
      leftmostColumn--, rightmostColumn++
    )
    {
      for(int row = 0; row < matrix.GetLength(0); row++)
      {
        if(matrix[row, leftmostColumn] != matrix[row, rightmostColumn])
        {
          if(++defects > 1) break;
        }
      }
    } 

    if(defects == 1) columnsToLeft = lineOfReflection.RightColumn;
  }

  return columnsToLeft;
}

int FindColumnsAboveLineOfReflectionWithDefect(char[,] matrix)
{
  (int TopRow, int BottomRow) lineOfReflection = (0, 1);
  int columnsAbove = 0;

  for( ; lineOfReflection.BottomRow < matrix.GetLength(0); lineOfReflection.TopRow++, lineOfReflection.BottomRow++)
  {
    int defects = 0;

    for
    (
      int topmostRow = lineOfReflection.TopRow, bottommostRow = lineOfReflection.BottomRow; 
      topmostRow >= 0 && bottommostRow < matrix.GetLength(0); 
      topmostRow--, bottommostRow++
    )
    {
      for(int column = 0; column < matrix.GetLength(1); column++)
      {
        if(matrix[topmostRow, column] != matrix[bottommostRow, column])
        {
          if(++defects > 1) break;
        }
      }
    } 

    if(defects == 1) columnsAbove = lineOfReflection.BottomRow;
  }


  return columnsAbove;
}