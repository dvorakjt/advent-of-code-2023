class SourceRange : Range {
  public long Modifier { get; private set; }

  public SourceRange(long start, long end, long modifier) : base(start, end)
  {
    Modifier = modifier;
  }

  public (Range? ProcessedRange, List<Range> UnprocessableRanges) ProcessRange(Range r)
  {
    List<Range> unprocessableRanges;
    Range? processedRange = GetModifiedProcessableRange(r);

    if(processedRange == null)
    {
      unprocessableRanges = new() { r };
      return (processedRange, unprocessableRanges);
    }

    unprocessableRanges = GetUnprocessableRanges(r);

    return (processedRange, unprocessableRanges);
  }

  private Range? GetModifiedProcessableRange(Range r)
  {
    //the range is wholly outside of this range
    if(r.End <= Start || r.Start >= End)
    {
      return null;
    }

    long start = Math.Max(r.Start, Start) + Modifier;
    long end = Math.Min(r.End, End) + Modifier;

    return new Range(start, end);
  }

  private List<Range> GetUnprocessableRanges(Range r)
  {
    List<Range> unprocessableRanges = new();

    if(r.Start < Start)
    {
      unprocessableRanges.Add(new Range(r.Start, Start));
    }
    if(r.End > End)
    {
      unprocessableRanges.Add(new Range(End, r.End));
    }

    return unprocessableRanges;
  }
}