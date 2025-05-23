using Bookmaster.Common.Domain;

namespace Bookmaster.Common.Features.Exceptions;

public sealed class BookmasterException : Exception
{
    public BookmasterException(string requestName, Error? error = default, Exception? innerException = default) 
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }
    public Error? Error { get; }
}
