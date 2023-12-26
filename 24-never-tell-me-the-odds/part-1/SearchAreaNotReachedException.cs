class SearchAreaNotReachedException : Exception 
{
  public SearchAreaNotReachedException()
    {
    }

    public SearchAreaNotReachedException(string message)
        : base(message)
    {
    }

    public SearchAreaNotReachedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}