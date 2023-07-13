using System.Net;

namespace AsyncDemo.MessageProcessor.Services;

public class ClientUpdaterException : Exception
{
    public ClientUpdaterException(string url, HttpStatusCode statusCode, string? errorJson) : base(
        $"{url} : {statusCode} - {errorJson}")
    {
    }
}