class SourceToDestinationMap {
  public List<Range> MappedSourceRanges = new List<Range>();

  public SourceToDestinationMap(string map)
  {
    List<string> mappings = new(map.Split('\n', StringSplitOptions.RemoveEmptyEntries));
    
    foreach(string mapping in mappings)
    {
      List<string> rangeInfo = new(mapping.Split(' ', StringSplitOptions.RemoveEmptyEntries));

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