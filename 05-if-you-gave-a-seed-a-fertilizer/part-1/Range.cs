struct Range : IComparable<Range> {
  public long Start { get; private set; }
  public long End { get; private set; }
  public long Modifier { get; private set; }

  public Range(long start, long end, long modifier)
  {
    Start = start;
    End = end;
    Modifier = modifier;
  }

  public int CompareTo(Range other)
  {
    return Start.CompareTo(other.Start);
  }
}