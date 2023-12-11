public class PointNotFoundOrInvalidException : Exception
{
    public PointNotFoundOrInvalidException()
    {
    }

    public PointNotFoundOrInvalidException(string message)
        : base(message)
    {
    }

    public PointNotFoundOrInvalidException(string message, Exception inner)
        : base(message, inner)
    {
    }
}