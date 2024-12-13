using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class CreateRoleCommandHandler(RoleManager<Domain.Entities.Role> roleManager) : IRequestHandler<CreateRoleCommand, Unit>
{
    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new global::MediaReview.Identity.Domain.Entities.Role
        {
            Name = request.Name,
            IsActive = request.IsActive
        };
        
        var result = await roleManager.CreateAsync(role);
        
        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to create role");
        
        return Unit.Value;
    }
}