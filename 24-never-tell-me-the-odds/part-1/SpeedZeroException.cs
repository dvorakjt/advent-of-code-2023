class SpeedZeroException : Exception 
{
  public SpeedZeroException()
    {
    }

    public SpeedZeroException(string message)
        : base(message)
    {
    }

    public SpeedZeroException(string message, Exception inner)
        : base(message, inner)
    {
    }
}