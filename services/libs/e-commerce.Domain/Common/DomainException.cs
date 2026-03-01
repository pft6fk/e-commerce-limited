namespace e_commerce.Domain.Common;

public class DomainException : Exception
{
    public DomainException()
    {
    }
    public DomainException(string message) : base(message)
    {
    }
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
