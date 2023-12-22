struct ColumnItem
{
  public int Row;
  public readonly bool IsRound;

  public ColumnItem(int row, bool isRound)
  {
    Row = row;
    IsRound = isRound;
  }
}