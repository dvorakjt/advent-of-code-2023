static class InputParser
{
  public static List<ArrangementCounter> GetIncompleteConditionRecordsFromInput(string[] input)
  {
    List<ArrangementCounter> incompleteConditionRecords = new();

    foreach(string line in input)
    {
      string[] record = line.Trim().Split(' ');
      string conditionRecord = record[0];
      List<int> expectedDamagedGroups = new(record[1].Split(',').Select(g => int.Parse(g)));

      ArrangementCounter incompleteConditionRecord = new(conditionRecord, expectedDamagedGroups);

      incompleteConditionRecords.Add(incompleteConditionRecord);
    }

    return incompleteConditionRecords;
  }
}