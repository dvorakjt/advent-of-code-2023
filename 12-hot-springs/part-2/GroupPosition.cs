class GroupPosition
{
    public int Start { get; private set; }
    public long PossibleArrangements = 0;

    public GroupPosition(int start)
    {
        Start = start;
    }
}