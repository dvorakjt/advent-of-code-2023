class SeedExtractor
{
  private const string SEEDS_MARKER = "seeds: ";
  public List<long> Seeds;

  public SeedExtractor(string input)
  {
    List<string> seeds = new(input.Substring(SEEDS_MARKER.Length, input.IndexOf('\n') - SEEDS_MARKER.Length).Split(' ', StringSplitOptions.RemoveEmptyEntries));
    Seeds = new List<long>(seeds.Select(long.Parse));
  }
}