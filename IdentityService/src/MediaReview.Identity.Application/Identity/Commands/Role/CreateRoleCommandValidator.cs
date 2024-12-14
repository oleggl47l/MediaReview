using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(RoleManager<Domain.Entities.Role> roleManager)
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MaximumLength(20)
            .WithMessage("Role name cannot exceed 20 characters.")
            .MustAsync(async (name, cancellationToken) =>
            {
                var existingRole = await roleManager.FindByNameAsync(name);
                return existingRole == null;
            })
            .WithMessage("A role with the given name already exists.");

        RuleFor(command => command.IsActive)
            .Must(value => value is bool)
            .WithMessage("IsActive must be a boolean value.");
    }
}
