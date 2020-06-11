using FluentValidation;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.StreetShipping)
                .MaximumLength(100).WithMessage("Street max 100 characters");

            RuleFor(o => o.HouseNrShipping)
                .MaximumLength(5).WithMessage("Nr max 5 characters");

            RuleFor(o => o.HouseBusShipping)
                .MaximumLength(4).WithMessage("Bus max 4 characters");

            RuleFor(o => o.PostalcodeShipping)
                .MaximumLength(6).WithMessage("Postalcode max 6 characters");

            RuleFor(o => o.CityShipping)
                .MaximumLength(100).WithMessage("City max 100 characters");
        }
    }
}