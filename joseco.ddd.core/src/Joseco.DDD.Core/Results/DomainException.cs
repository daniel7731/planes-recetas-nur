namespace Joseco.DDD.Core.Results;

public class DomainException(Error Error) : Exception
{
    public Error Error { get; } = Error;
}