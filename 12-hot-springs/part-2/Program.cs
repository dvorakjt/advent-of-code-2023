string conditionRecord = "?###??????????###??????????###??????????###??????????###????????";
int[] groups = { 3, 2, 1,  3, 2, 1,  3, 2, 1,  3, 2, 1,  3, 2, 1 };

//transform all groups except the last group into strings of one or more # and one .
//last group is transformed into a string of #
List<string> transformedGroups = TransformGroups(groups);

//create a new list of list of ranges
List<List<Range>> possibleRangesPerTransformedGroup = GetPossibleRangesPerTransformedGroup(transformedGroups, conditionRecord);

//evaluate starting and ending ranges, remove any that don't fit the criteria
RemoveInvalidLeftmostRangesAndSetInitialCounts(possibleRangesPerTransformedGroup, transformedGroups[0], conditionRecord);
RemoveInvalidRightmostRanges(possibleRangesPerTransformedGroup, transformedGroups.Last(), conditionRecord);

//now evaluate each list of ranges against the preceding list of ranges
//for each list of ranges that is further left than the current range,
//create a string like ##....### and compare against a substring of the condition record
//if it is valid,
//add that range's possibility count to the current ranges possibility count
//if at the end of this process, the possibility count for a range is greater than zero, add it to filtered ranges for that range

for(int i = 1; i < possibleRangesPerTransformedGroup.Count; i++)
{
  List<Range> filteredRanges = new();

  for(int j = 0; j < possibleRangesPerTransformedGroup[i].Count; j++)
  {
    var range = possibleRangesPerTransformedGroup[i][j];
    foreach(var previousRange in possibleRangesPerTransformedGroup[i -1])
    {
      if(previousRange.Start + previousRange.Length <= range.Start)
      {
        int charsToFill = range.Start - (previousRange.Start + previousRange.Length);
        string test = transformedGroups[i - 1] + new string('.', charsToFill) + transformedGroups[i];
        if(IsValidSubstring(test, conditionRecord, previousRange.Start))
        {
          range.PossibleArrangements += previousRange.PossibleArrangements;
        } 
      }
    }
    if(range.PossibleArrangements > 0) filteredRanges.Add(range);
  }

  possibleRangesPerTransformedGroup[i] = filteredRanges;
}

Console.WriteLine(possibleRangesPerTransformedGroup.Last().Select(r => r.PossibleArrangements).Sum());

List<string> TransformGroups(IEnumerable<int> groups)
{
  List<string> transformedGroups = groups.Take(groups.Count() - 1).Select((group) => new string('#', group) + '.').ToList();
  transformedGroups.Add(new string('#', groups.Last()));

  return transformedGroups;
}

List<List<Range>> GetPossibleRangesPerTransformedGroup(List<string> transformedGroups, string conditionRecord)
{
  int furthestLeftPosition = 0;
  int remainingRequiredLength = transformedGroups.Select(g => g.Length).Sum();

  List<List<Range>> rangesPerGroup = new();

  foreach (var group in transformedGroups)
  {
    int furthestRightPosition = conditionRecord.Length - remainingRequiredLength;

    List<Range> ranges = new();

    for (int i = furthestLeftPosition; i <= furthestRightPosition; i++)
    {
        ranges.Add(new Range(i, group.Length));
    }

    rangesPerGroup.Add(ranges);

    furthestLeftPosition += group.Length;
    remainingRequiredLength -= group.Length;
  }

  return rangesPerGroup;
}

void RemoveInvalidLeftmostRangesAndSetInitialCounts(List<List<Range>> possibleRangesPerGroup, string transformedGroup, string conditionRecord)
{
  List<Range> leftmostRanges = possibleRangesPerGroup[0];
  List<Range> filteredRanges = new();

  for(int i = 0; i < leftmostRanges.Count; i++)
  {
    Range range = leftmostRanges[i];
    string test = new string('.', i) + transformedGroup;
    if(IsValidSubstring(test, conditionRecord, 0))
    {
      range.PossibleArrangements++;
      filteredRanges.Add(range);
    }
  }

  possibleRangesPerGroup[0] = filteredRanges;
}


void RemoveInvalidRightmostRanges(List<List<Range>> possibleRangesPerGroup, string transformedGroup, string conditionRecord)
{
  List<Range> rightmostRanges = possibleRangesPerGroup[possibleRangesPerGroup.Count - 1];
  List<Range> filteredRanges = new();

  
  foreach(var range in rightmostRanges)
  {
    int charactersToFill = conditionRecord.Length - range.Start - range.Length;
    string test = transformedGroup + new string('.', charactersToFill);
    if(IsValidSubstring(test, conditionRecord, range.Start))
    {
      filteredRanges.Add(range);
    }
  }

  possibleRangesPerGroup[possibleRangesPerGroup.Count - 1] = filteredRanges;
}

bool IsValidSubstring(string substring, string conditionRecord, int start)
{
  string conditionRecordSubstring = conditionRecord.Substring(start, substring.Length);

  for(int i = 0; i < substring.Length; i++)
  {
    if
    (
      (substring[i] == '#' && conditionRecordSubstring[i] == '.') ||
      (substring[i] == '.' && conditionRecordSubstring[i] == '#')
    )
    {
      return false;
    }
  }

  return true;
}