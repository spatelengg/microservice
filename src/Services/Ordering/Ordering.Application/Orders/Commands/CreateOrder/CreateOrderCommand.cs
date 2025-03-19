
namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommad(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommad>
{
    public CreateOrderCommandValidator() {
        RuleFor(x=>x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x=>x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x=>x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not");
    }
}