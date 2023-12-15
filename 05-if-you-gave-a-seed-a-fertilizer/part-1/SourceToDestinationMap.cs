class SourceToDestinationMap {
  public List<Range> MappedSourceRanges = new List<Range>();

  public SourceToDestinationMap(string map)
  {
    List<string> mappings = map.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
    
    foreach(string mapping in mappings)
    {
      List<string> rangeInfo = mapping.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

      long destinationRangeStart = long.Parse(rangeInfo[0]);
      long sourceRangeStart = long.Parse(rangeInfo[1]);
      long length = long.Parse(rangeInfo[2]);
      
      long sourceRangeEnd = sourceRangeStart + length;
      long modifier = destinationRangeStart - sourceRangeStart;

      Range r = new(sourceRangeStart, sourceRangeEnd, modifier);

      MappedSourceRanges.Add(r);
    }

    MappedSourceRanges.Sort();
  }

  public override string ToString()
  {
    return string.Join('\n', MappedSourceRanges.Select(r => r.ToString()));
  }
}