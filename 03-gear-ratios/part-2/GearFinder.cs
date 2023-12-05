/*
  This class transforms the input into a matrix containing totals where each digit in the input was found. At the same time,
  it notes locations of possible gears. It then checks the cells surrounding these locations for numbers. Cells with 2 adjacent numbers
  are gears.
*/
class GearFinder {
  private string[,] Matrix;
  private List<(int Row, int Column)> PossibleGearLocations = new List<(int Row, int Column)>();

  public GearFinder (string[] engineSchematic)
  {
    this.Matrix = new string[engineSchematic.Length , engineSchematic[0].Length];
    this.TransformMatrixAndFindPossibleGears(engineSchematic);
  }

  private void TransformMatrixAndFindPossibleGears(string[] engineSchematic)
  {
    for(int i = 0; i < this.Matrix.GetLength(0); i++)
    {
      string currentNumber = "";
      int j = 0;

      for( ; j < this.Matrix.GetLength(1); j++)
      {
        char c = engineSchematic[i][j];

        if(IsDigit(c))
        {
          currentNumber += c;
          
          continue;
        }

        if(currentNumber.Length > 0)
        {
          ReplaceMatrixDigitsWithTotal(currentNumber, i, j);

          currentNumber = "";
        }

        if(c == '*')
        {
          PossibleGearLocations.Add((i, j));
        }
      }

      if(currentNumber.Length > 0)
      {
        ReplaceMatrixDigitsWithTotal(currentNumber, i, j);
      }
    }
  }

  public List<List<int>> FindGears()
  {
    List<List<int>> gears = new List<List<int>>();

    foreach(var location in PossibleGearLocations)
    {
      var (Row, Column) = location;
      var adjacentNumbers = FindAdjacentNumbers(Row, Column);

      if(adjacentNumbers.Count == 2)
      {
        gears.Add(adjacentNumbers);
      }
    }

    return gears;
  }

  private List<int> FindAdjacentNumbers (int row, int column)
  {
    List<int> adjacentNumbers = new List<int>();

    //check above
    if(row > 0)
    {
      int rowAbove = row - 1;
      int start = column > 0 ? column - 1 : column;
      int end = column + 1 < Matrix.GetLength(1) ? column + 1 : column;
      bool foundNumberToLeft = false;

      for(int i = start; i <= end; i++)
      {
        if(IsNumber(rowAbove, i))
        {
          if(!foundNumberToLeft)
          {
            int partNumber = int.Parse(Matrix[rowAbove,i]);
            adjacentNumbers.Add(partNumber);
            foundNumberToLeft = true;
          }
        }
        else
        {
          foundNumberToLeft = false;
        }
      }
    }

    //check left
    if(column > 0)
    {
      int columnOnLeft = column - 1;
      if(IsNumber(row, columnOnLeft))
      {
        int partNumber = int.Parse(Matrix[row, columnOnLeft]);
        adjacentNumbers.Add(partNumber);
      }
    }

    //check right
    int columnOnRight = column + 1;
    if(columnOnRight < Matrix.GetLength(1))
    {
      if(IsNumber(row, columnOnRight))
      {
        int partNumber = int.Parse(Matrix[row, columnOnRight]);
        adjacentNumbers.Add(partNumber);
      }
    }

    //check below
    int rowBelow = row + 1;
    if(rowBelow < Matrix.GetLength(0))
    {
      int start = column > 0 ? column - 1 : column;
      int end = column + 1 < Matrix.GetLength(1) ? column + 1 : column;
      bool foundNumberToLeft = false;
      
      for(int i = start; i <= end; i++)
      {
        if(IsNumber(rowBelow, i))
        {
          if(!foundNumberToLeft)
          {
            int partNumber = int.Parse(Matrix[rowBelow,i]);
            adjacentNumbers.Add(partNumber);
            foundNumberToLeft = true;
          }
        }
        else
        {
          foundNumberToLeft = false;
        }
      }
    }

    return adjacentNumbers;
  }

  private bool IsNumber(int row, int column)
  {
    return Matrix[row,column] != null;
  }

  private bool IsDigit(char c)
  {
    int d = c - '0';
    return d >= 0 && d <= 9;
  }

  private void ReplaceMatrixDigitsWithTotal(string total, int row, int column)
  {
    int startingColumn = column - total.Length;

    while(true)
    {
      if(--column < startingColumn) break;

      Matrix[row, column] = total;
    }
  } 
}