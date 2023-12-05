class Range {
  public long Start { get; protected set; }
  public long End { get; protected set; }
  public Range(long start, long end)
  {
    Start = start;
    End = end;
  }
}