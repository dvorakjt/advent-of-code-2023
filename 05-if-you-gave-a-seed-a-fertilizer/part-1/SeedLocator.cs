class SeedLocator
{
  private List<SourceToDestinationMap> SourceToDestinationMaps;

  public SeedLocator(List<SourceToDestinationMap> sourceToDestinationMaps)
  {
    SourceToDestinationMaps = sourceToDestinationMaps;
  }

  public long GetLocation(long seed)
  {
    long destination = seed;

    foreach(SourceToDestinationMap sourceToDestinationMap in SourceToDestinationMaps)
    {
      destination = ConvertToNextDestination(sourceToDestinationMap, destination);
    }

    return destination;
  }

  private long ConvertToNextDestination(SourceToDestinationMap map, long previousValue)
  {
    foreach(var sourceRange in map.MappedSourceRanges)
    {
      //previous value is outside all ranges
      if(previousValue < sourceRange.Start) break;
      
      //previous value is within the current range
      if(previousValue < sourceRange.End)
      {
        return previousValue + sourceRange.Modifier;
      }
    }

    //previous value is outside all ranges
    return previousValue;
  }
}