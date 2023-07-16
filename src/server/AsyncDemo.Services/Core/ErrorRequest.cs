using MediatR;

namespace AsyncDemo.Services.Core;

public class ErrorRequest<T> : IRequest where T : IRequest
{
    public ErrorRequest(T request, Exception ex)
    {
        Request = request;
        Exception = ex;
    }

    public T Request { get; }

    public Exception Exception { get; }
}