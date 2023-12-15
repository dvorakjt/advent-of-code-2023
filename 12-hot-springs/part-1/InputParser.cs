static class InputParser
{
  public static List<IncompleteConditionRecord> GetIncompleteConditionRecordsFromInput(string[] input)
  {
    List<IncompleteConditionRecord> incompleteConditionRecords = new();

    foreach(string line in input)
    {
      string[] record = line.Trim().Split(' ');
      char[] conditionRecord = record[0].ToCharArray();
      List<int> expectedDamagedGroups = record[1].Split(',').Select(g => int.Parse(g)).ToList();

      IncompleteConditionRecord incompleteConditionRecord = new(conditionRecord, expectedDamagedGroups);

      incompleteConditionRecords.Add(incompleteConditionRecord);
    }

    return incompleteConditionRecords;
  }
}