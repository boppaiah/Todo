using FluentValidation;
namespace TodoAPI.Todos.CreateTodo
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Todo name is required.")
                .MaximumLength(150).WithMessage("Todo name cannot be more than 150 charecters");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Todo description is required.")
                 .MaximumLength(200).WithMessage("Todo description cannot be more than 200 charecters");

            RuleFor(x => x.Priority).NotNull().WithMessage("Todo priority is required.")
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(3).WithMessage("Can only be low=1, medium=2 or high=3");

            RuleFor(x => x.StartDate).NotNull().WithMessage("Todo start date is required.")
                .LessThan(x => x.EndDate).WithMessage("Todo start date cannot be greater than end date.");

            RuleFor(x => x.EndDate).NotNull().WithMessage("Todo end date is required.")
             .GreaterThan(x => x.StartDate).WithMessage("Todo end date cannot be less than start date.");
        }
    }
}
