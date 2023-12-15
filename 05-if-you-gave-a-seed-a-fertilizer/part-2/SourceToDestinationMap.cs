class SourceToDestinationMap {
  public List<SourceRange> MappedSourceRanges = new List<SourceRange>();

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

      SourceRange r = new(sourceRangeStart, sourceRangeEnd, modifier);

      MappedSourceRanges.Add(r);
    }
  }

  public List<Range> MapSourceRangesToDestinationRanges(List<Range> sourceRanges)
  {
    List<Range> destinationRanges = new();
    List<Range> unprocessedRanges = sourceRanges;
    

    foreach(var sourceRange in MappedSourceRanges)
    {
      List<Range> updatedUnprocessedRanges = new();

      foreach(var unprocessedRange in unprocessedRanges)
      {
        var (ProcessedRange, UnprocessableRanges) = sourceRange.ProcessRange(unprocessedRange);
        
        if(ProcessedRange != null) destinationRanges.Add(ProcessedRange);

        updatedUnprocessedRanges.AddRange(UnprocessableRanges);
      }

      unprocessedRanges = updatedUnprocessedRanges;
    }

    destinationRanges.AddRange(unprocessedRanges);
  
    return destinationRanges;
  }
}