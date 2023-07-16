using FluentValidation;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.AssemblyName)
            .NotEmpty()
            .NotNull();
    }
}