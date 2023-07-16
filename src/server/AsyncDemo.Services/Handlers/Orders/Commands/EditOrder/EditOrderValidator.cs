using FluentValidation;

namespace AsyncDemo.Services.Handlers.Orders.Commands.EditOrder;

public class EditOrderValidator : AbstractValidator<EditOrderRequest>
{
    public EditOrderValidator()
    {
        RuleFor(x => x.AssemblyName)
            .NotEmpty()
            .NotNull();
    }
}