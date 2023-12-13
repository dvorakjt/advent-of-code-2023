class IncompleteConditionRecord
{
  private List<int> ExpectedDamagedSpringGroups;
  public int PossibleGroupArrangementsCount { get; private set; } = 0;

  public IncompleteConditionRecord(char[] conditionRecord, List<int> expectedDamagedSpringGroups)
  {
    ExpectedDamagedSpringGroups = expectedDamagedSpringGroups;
    CountPossibleGroupArrangements(conditionRecord);
  }

  private void CountPossibleGroupArrangements(char[] conditionRecord)
  {
    CountPossibleGroupArrangements(conditionRecord, 0);
  }

  private void CountPossibleGroupArrangements(char[] conditionRecord, int index)
  {
    bool locatedQuestionMark = false;

    for(; index < conditionRecord.Length; index++)
    {
      if(conditionRecord[index] == '?')
      {
        locatedQuestionMark = true;
        break;
      }
    }

    if(!locatedQuestionMark)
    {
      PossibleGroupArrangementsCount += IsValidConditionRecord(conditionRecord) ? 1 : 0;
    }
    else
    {
      char[] conditionRecordWithOperationalSpring = new char[conditionRecord.Length]; 
      char[] conditionRecordWithBrokenSpring = new char[conditionRecord.Length];

      Array.Copy(conditionRecord, conditionRecordWithOperationalSpring, conditionRecord.Length);
      Array.Copy(conditionRecord, conditionRecordWithBrokenSpring, conditionRecord.Length);

      conditionRecordWithOperationalSpring[index] = '.';
      conditionRecordWithBrokenSpring[index] = '#';
      
      CountPossibleGroupArrangements(conditionRecordWithOperationalSpring, index + 1);
      CountPossibleGroupArrangements(conditionRecordWithBrokenSpring, index + 1);
    }
  }

  private bool IsValidConditionRecord(char[] conditionRecord)
  {
    List<int> actualDamagedSpringGroups = new();

    int currentDamagedGroupCount = 0;

    for(int i = 0; i < conditionRecord.Length; i++)
    {
      if(conditionRecord[i] == '.' && currentDamagedGroupCount > 0)
      {
        actualDamagedSpringGroups.Add(currentDamagedGroupCount);
        currentDamagedGroupCount = 0;
      }
      else if(conditionRecord[i] == '#')
      {
        currentDamagedGroupCount++;
      }
    }

    if(currentDamagedGroupCount > 0)
    {
      actualDamagedSpringGroups.Add(currentDamagedGroupCount);
    }

    if(actualDamagedSpringGroups.Count != ExpectedDamagedSpringGroups.Count) return false;

    for(int i = 0; i < actualDamagedSpringGroups.Count; i++)
    {
      if(actualDamagedSpringGroups[i] != ExpectedDamagedSpringGroups[i]) return false;
    }

    return true;
  }
}