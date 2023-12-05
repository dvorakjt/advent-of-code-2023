class SeedRangesExtractor
{
  private const string SEEDS_MARKER = "seeds: ";
  public List<Range> SeedRanges = new();

  public SeedRangesExtractor(string input)
  {
    List<string> values = new(input.Substring(SEEDS_MARKER.Length, input.IndexOf('\n') - SEEDS_MARKER.Length).Split(' ').Where(s => !string.IsNullOrEmpty(s)));
  
    for(int i = 0; i < values.Count; i += 2)
    {
      long start = long.Parse(values[i]);
      long length = long.Parse(values[i + 1]);

      Range seedRange = new Range(start, start + length);

      SeedRanges.Add(seedRange);
    }
  }
}