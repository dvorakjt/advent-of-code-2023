struct RowItem 
{
  public int Column;
  public readonly bool IsRound;

  public RowItem(int column, bool isRound)
  {
    Column = column;
    IsRound = isRound;
  }
}