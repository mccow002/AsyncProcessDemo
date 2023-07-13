namespace AsyncDemo.Services.Core;

public interface ICurrentConnectionIdService
{
    string ConnectionId { get; }
}