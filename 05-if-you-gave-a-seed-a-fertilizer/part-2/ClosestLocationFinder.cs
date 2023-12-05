class ClosestLocationFinder
{
  public long ClosestLocation { get; private set; }
  
  public ClosestLocationFinder(List<Range> seedRanges, List<SourceToDestinationMap> sourceToDestinationMaps)
  {
    foreach(var sourceToDestinationMap in sourceToDestinationMaps)
    {
      seedRanges = sourceToDestinationMap.MapSourceRangesToDestinationRanges(seedRanges);
    }

    ClosestLocation= seedRanges.OrderBy(l => l.Start).First().Start;
  }
}