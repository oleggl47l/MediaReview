using FluentValidation;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");
    }
}