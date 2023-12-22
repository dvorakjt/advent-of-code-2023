class Matrix
{
  List<LinkedList<RowItem>> Rows = new();
  public List<LinkedList<ColumnItem>> Columns = new();

  Dictionary<string, int> Hashes = new();

  public Matrix(int rows, int columns)
  {
    for(int i = 0; i < rows; i++)
    {
      Rows.Add(new());
    }
    for(int i = 0; i < columns; i++)
    {
      Columns.Add(new());
    }
  }

  public void AddRoundRock(int row, int column)
  {
    ColumnItem columnItem = new(row, true);
    Columns[column].AddLast(columnItem);
  }

  public void AddSquareRock(int row, int column)
  {
    ColumnItem columnItem = new(row, false);
    Columns[column].AddLast(columnItem);
  }

  public void PerformCycle()
  {
    TiltNorth();
    TiltWest();
    TiltSouth();
    TiltEast();
  }

  public void TiltNorth()
   {
    for(int i = 0; i < Columns.Count; i++)
    {
      var column = Columns[i];
      int firstOpenRow = 0;

      var columnNode = column.First;

      while(columnNode != null)
      {
        var columnItem = columnNode.Value;
        int row = columnItem.IsRound ? firstOpenRow : columnItem.Row;
        firstOpenRow = row + 1;

        Rows[row].AddLast(new RowItem(i, columnItem.IsRound));

        column.RemoveFirst();
        columnNode = column.First;
      }
    }
  }

  public void TiltSouth()
   {
    for(int i = 0; i < Columns.Count; i++)
    {
      var column = Columns[i];
      int firstOpenRow = Rows.Count - 1;

      var columnNode = column.Last;

      while(columnNode != null)
      {
        var columnItem = columnNode.Value;
        int row = columnItem.IsRound ? firstOpenRow : columnItem.Row;
        firstOpenRow = row - 1;

        Rows[row].AddLast(new RowItem(i, columnItem.IsRound));

        column.RemoveLast();
        columnNode = column.Last;
      }
    }
  }

  public void TiltWest()
   {
    for(int i = 0; i < Rows.Count; i++)
    {
      var row = Rows[i];
      int firstOpenColumn = 0;

      var rowNode = row.First;

      while(rowNode != null)
      {
        var rowItem = rowNode.Value;
        int column = rowItem.IsRound ? firstOpenColumn : rowItem.Column;
        firstOpenColumn = column + 1;

        Columns[column].AddLast(new ColumnItem(i, rowItem.IsRound));

        row.RemoveFirst();
        rowNode = row.First;
      }
    }
  }

  public void TiltEast()
   {
    for(int i = 0; i < Rows.Count; i++)
    {
      var row = Rows[i];
      int firstOpenColumn = Columns.Count - 1;

      var rowNode = row.Last;

      while(rowNode != null)
      {
        var rowItem = rowNode.Value;
        int column = rowItem.IsRound ? firstOpenColumn : rowItem.Column;
        firstOpenColumn = column - 1;

        Columns[column].AddLast(new ColumnItem(i, rowItem.IsRound));

        row.RemoveLast();
        rowNode = row.Last;
      }
    }
  }

  public bool CheckForCycleAndAddHash(int i)
  {
    string hash = MatrixHashGenerator.HashMatrix(Columns);

    bool matchFound = Hashes.ContainsKey(hash);

    if(!matchFound) Hashes.Add(hash, i);
    else
    {
      Hashes.Clear();
      Hashes.Add(hash, i);
    }

    return matchFound;
  }

public int CalculateLoad()
  {
  int load = 0;

  foreach(var column in Columns)
  {
    var columnNode = column.First;
    
    while(columnNode != null)
    {
      if(columnNode.Value.IsRound)
      {

      load += Rows.Count - columnNode.Value.Row;
      }
      columnNode = columnNode.Next;
    }

  }

  return load;
  }

  public int CalculateLoadRows()
  {
    int load = 0;

  for(int i = 0; i < Rows.Count; i++)
  {
    load += Rows[i].Count(item => item.IsRound) * (Rows.Count - i);
  }

  return load;
  
  }
}

