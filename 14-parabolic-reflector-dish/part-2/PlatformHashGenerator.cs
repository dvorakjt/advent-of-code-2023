using System.Text;

static class PlatformHashGenerator
{
  private const string BASE_100 = 
    "0123456789!\"#$%&'()*+,-./:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~ÿĀāĂăĄ";
  private const char COLUMN_SEPARATOR = ' ';
  private const char SQUARE_BLOCK = (char)('Ą' + 1);
  private const char ROUND_BLOCK = (char)('Ą' + 2);

  public static string HashPlatform(List<LinkedList<ColumnRock>> columns)
  {
    List<string> columnHashes = new();

    foreach(var column in columns)
    {
      StringBuilder colStringBuilder = new();

      var columnNode = column.First;

      while(columnNode != null)
      {
        colStringBuilder.Append(HashRow(columnNode.Value.Row));
        colStringBuilder.Append(HashRock(columnNode.Value.IsRound));
        columnNode = columnNode.Next;  
      }

      columnHashes.Add(colStringBuilder.ToString());
    }

    return string.Join(COLUMN_SEPARATOR, columnHashes);
  }

  private static char HashRow(int row)
  {
    return BASE_100[row];
  }

  private static char HashRock(bool isRound)
  {
    return isRound ? ROUND_BLOCK : SQUARE_BLOCK;
  }
}