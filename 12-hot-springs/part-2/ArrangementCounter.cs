class ArrangementCounter
{
  private string ConditionRecord;
  private List<string> Groups;
  private List<List<GroupPosition>> PossiblePositionsPerGroup;

  public long PossibleArrangements { get; private set; }

  public ArrangementCounter(string conditionRecord, List<int> groupCounts)
  {
    ConditionRecord = conditionRecord;
    Groups = TransformGroups(groupCounts);
    PossiblePositionsPerGroup = GetPossiblePositionsPerGroup();
    RemoveInvalidPositionsFromFirstGroupAndSetInitialCounts();
    RemoveInvalidPositionsFromLastGroup();
    DeterminePossibleArrangementCounts();
    PossibleArrangements = SumPossibleArrangementCounts();
  }

  private List<string> TransformGroups(List<int> groupCounts)
  {
    List<string> groups = groupCounts.Take(groupCounts.Count() - 1).Select((groupCount) => new string('#', groupCount) + '.').ToList();
    groups.Add(new string('#', groupCounts.Last()));

    return groups;
  }

  private List<List<GroupPosition>> GetPossiblePositionsPerGroup()
  {
    int furthestLeftPosition = 0;
    int remainingRequiredLength = Groups.Select(g => g.Length).Sum();

    List<List<GroupPosition>> positionsPerGroup = new();

    foreach (var group in Groups)
    {
      int furthestRightPosition = ConditionRecord.Length - remainingRequiredLength;

      List<GroupPosition> positions = new();

      for (int i = furthestLeftPosition; i <= furthestRightPosition; i++)
      {
        positions.Add(new GroupPosition(i));
      }

      positionsPerGroup.Add(positions);

      furthestLeftPosition += group.Length;
      remainingRequiredLength -= group.Length;
    }

    return positionsPerGroup;
  }

  private void RemoveInvalidPositionsFromFirstGroupAndSetInitialCounts()
  {
    List<GroupPosition> positions = PossiblePositionsPerGroup[0];
    List<GroupPosition> filteredPositions = new();

    for(int i = 0; i < positions.Count; i++)
    {
      GroupPosition position = positions[i];
      string recordSegment = new string('.', i) + Groups[0];

      if(IsValidRecordSegment(recordSegment, 0))
      {
        position.PossibleArrangements++;
        filteredPositions.Add(position);
      }
    }

    PossiblePositionsPerGroup[0] = filteredPositions;
  }


  private void RemoveInvalidPositionsFromLastGroup()
  {
    List<GroupPosition> positions = PossiblePositionsPerGroup.Last();
    List<GroupPosition> filteredPositions = new();
    string group = Groups.Last();

    foreach(var position in positions)
    {
      int addedOperationalSprings = ConditionRecord.Length - position.Start - group.Length;
      string recordSegment = group + new string('.', addedOperationalSprings);
      if(IsValidRecordSegment(recordSegment, position.Start))
      {
        filteredPositions.Add(position);
      }
    }

    PossiblePositionsPerGroup[PossiblePositionsPerGroup.Count - 1] = filteredPositions;
  }

  private bool IsValidRecordSegment(string segment, int start)
  {
    string conditionRecordSegment = ConditionRecord.Substring(start, segment.Length);

    for(int i = 0; i < segment.Length; i++)
    {
      if
      (
        (segment[i] == '#' && conditionRecordSegment[i] == '.') ||
        (segment[i] == '.' && conditionRecordSegment[i] == '#')
      )
      {
        return false;
      }
    }

    return true;
  }

  private void DeterminePossibleArrangementCounts()
  {
    for(int i = 1; i < PossiblePositionsPerGroup.Count; i++)
    {
      List<GroupPosition> filteredPositions = new();

      for(int j = 0; j < PossiblePositionsPerGroup[i].Count; j++)
      {
        var position = PossiblePositionsPerGroup[i][j];

        foreach(var previousPosition in PossiblePositionsPerGroup[i -1])
        {
          string previousGroup = Groups[i - 1];

          if(previousPosition.Start + previousGroup.Length <= position.Start)
          {
            int addedOperationalSprings = position.Start - (previousPosition.Start + previousGroup.Length);
            string recordSegment = new string('.', addedOperationalSprings) + Groups[i];
            int segmentStart = previousPosition.Start + previousGroup.Length;

            if(IsValidRecordSegment(recordSegment, segmentStart))
            {
              position.PossibleArrangements += previousPosition.PossibleArrangements;
            } 
          }
        }
        if(position.PossibleArrangements > 0) filteredPositions.Add(position);
      }

      PossiblePositionsPerGroup[i] = filteredPositions;
    }
  }

  private long SumPossibleArrangementCounts()
  {
    return PossiblePositionsPerGroup.Last().Select(r => r.PossibleArrangements).Sum();
  }
}