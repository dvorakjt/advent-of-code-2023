class Range
{
    public int Start { get; private set; }
    public int Length { get; private set; }
    public long PossibleArrangements = 0;

    public Range(int start, int length)
    {
        Start = start;
        Length = length;
    }
}