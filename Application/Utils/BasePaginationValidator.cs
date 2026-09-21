using FluentValidation;

namespace Application.Utils
{
    public class BasePaginationValidator<T> : AbstractValidator<T> where T : PaginationQuery
    {
        public BasePaginationValidator()
        {
            RuleFor(command => command.Offset).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Limit).GreaterThan(0);
        }
    }
}
