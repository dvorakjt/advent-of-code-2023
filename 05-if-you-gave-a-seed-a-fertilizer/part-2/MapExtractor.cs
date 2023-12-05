using System.Text.RegularExpressions;

class MapExtractor 
{
  private const string PATTERN = @"(?<=map:\n)((\d+\s+\d+\s+\d+\n{0,1}))+";
  public List<SourceToDestinationMap> SourceToDestinationMaps = new();

  public MapExtractor(string input)
  {
    var matches = new Regex(PATTERN).Matches(input);

    foreach(Match match in matches)
    {
      string mapValues = match.Groups[0].Value;
      
      SourceToDestinationMap map = new(mapValues);

      SourceToDestinationMaps.Add(map);
    }
  }

}
