class Platform
{
  private List<LinkedList<RowRock>> Rows = new();
  private List<LinkedList<ColumnRock>> Columns = new();
  private Dictionary<string, int> Hashes = new();

  public Platform(int rows, int columns)
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
    ColumnRock columnRock = new(row, true);
    Columns[column].AddLast(columnRock);
  }

  public void AddSquareRock(int row, int column)
  {
    ColumnRock columnRock = new(row, false);
    Columns[column].AddLast(columnRock);
  }

  public int CalculateLoadAfter1BillionCycles()
  {
    var (preCycleIterations, cycleLength) = PerformCycleUntilMacroCycleDetected();

    int cyclesRemaining = ((int)1e9 - preCycleIterations) % cycleLength;

    for(int i = 0; i < cyclesRemaining; i++)
    {
      PerformCycle();
    }

    return CalculateLoad();
  }


  private (int preCycleIterations, int cycleLength) PerformCycleUntilMacroCycleDetected()
  { 
    int cycle = 1;

    while(true)
    {
      PerformCycle();
      string hash = PlatformHashGenerator.HashPlatform(Columns);

      if(CheckForCycleAndAddHash(hash, cycle))
      {
        int preCycleIterations = Hashes[hash];
        int cycleLength = cycle - preCycleIterations;

        return (preCycleIterations, cycleLength);
      }

      cycle++;
    }
  }

  private void PerformCycle()
  {
    TiltNorth();
    TiltWest();
    TiltSouth();
    TiltEast();
  }

  private void TiltNorth()
   {
    for(int i = 0; i < Columns.Count; i++)
    {
      var column = Columns[i];
      int firstOpenRow = 0;

      var columnNode = column.First;

      while(columnNode != null)
      {
        var columnRock = columnNode.Value;
        int row = columnRock.IsRound ? firstOpenRow : columnRock.Row;
        firstOpenRow = row + 1;

        Rows[row].AddLast(new RowRock(i, columnRock.IsRound));

        column.RemoveFirst();
        columnNode = column.First;
      }
    }
  }

  private void TiltSouth()
   {
    for(int i = 0; i < Columns.Count; i++)
    {
      var column = Columns[i];
      int firstOpenRow = Rows.Count - 1;

      var columnNode = column.Last;

      while(columnNode != null)
      {
        var columnRock = columnNode.Value;
        int row = columnRock.IsRound ? firstOpenRow : columnRock.Row;
        firstOpenRow = row - 1;

        Rows[row].AddLast(new RowRock(i, columnRock.IsRound));

        column.RemoveLast();
        columnNode = column.Last;
      }
    }
  }

  private void TiltWest()
   {
    for(int i = 0; i < Rows.Count; i++)
    {
      var row = Rows[i];
      int firstOpenColumn = 0;

      var rowNode = row.First;

      while(rowNode != null)
      {
        var rowRock = rowNode.Value;
        int column = rowRock.IsRound ? firstOpenColumn : rowRock.Column;
        firstOpenColumn = column + 1;

        Columns[column].AddLast(new ColumnRock(i, rowRock.IsRound));

        row.RemoveFirst();
        rowNode = row.First;
      }
    }
  }

  private void TiltEast()
   {
    for(int i = 0; i < Rows.Count; i++)
    {
      var row = Rows[i];
      int firstOpenColumn = Columns.Count - 1;

      var rowNode = row.Last;

      while(rowNode != null)
      {
        var rowRock = rowNode.Value;
        int column = rowRock.IsRound ? firstOpenColumn : rowRock.Column;
        firstOpenColumn = column - 1;

        Columns[column].AddLast(new ColumnRock(i, rowRock.IsRound));

        row.RemoveLast();
        rowNode = row.Last;
      }
    }
  }

  private bool CheckForCycleAndAddHash(string hash, int i)
  {
    bool matchFound = Hashes.ContainsKey(hash);

    if(!matchFound) Hashes.Add(hash, i);

    return matchFound;
  }

  private int CalculateLoad()
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
}

