using FluentValidation;

namespace AsyncDemo.Services.Handlers.Orders.Commands;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.AssemblyName)
            .NotEmpty()
            .NotNull();
    }
}