using Rebus.Exceptions;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public class SimulatedException : Exception, IFailFastException
{
    public SimulatedException() : base("This is a simulated exception!")
    {
        
    }
}