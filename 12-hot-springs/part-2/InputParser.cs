static class InputParser
{
  private const int FOLDS = 5;

  public static List<IncompleteConditionRecord> GetIncompleteConditionRecordsFromInput(string[] input)
  {
    List<IncompleteConditionRecord> incompleteConditionRecords = new();

    foreach(string line in input)
    {
      string[] record = line.Trim().Split(' ');
      string unfoldedConditionRecord = GetUnfoldedConditionRecord(record[0]);
      List<int> expectedDamagedGroups = ParseGroups(record[1]);
      List<int> unfoldedGroups = GetUnfoldedGroups(expectedDamagedGroups);

      IncompleteConditionRecord incompleteConditionRecord = new(unfoldedConditionRecord, unfoldedGroups);

      incompleteConditionRecords.Add(incompleteConditionRecord);
    }

    return incompleteConditionRecords;
  }

  private static string GetUnfoldedConditionRecord(string record)
  {
    string[] recordCopies = new string[FOLDS];
    Array.Fill(recordCopies, record);

    return string.Join('?', recordCopies);
  }

  private static List<int> ParseGroups(string groups)
  {
    return groups.Split(',').Select(g => int.Parse(g)).ToList();
  }

  private static List<int> GetUnfoldedGroups(List<int> groups)
  {
    List<int> unfoldedGroups = new();
  
    for(int i = 0; i < FOLDS; i++)
    {
      unfoldedGroups.AddRange(groups);
    }

    return unfoldedGroups;
  }

}