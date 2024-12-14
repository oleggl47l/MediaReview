using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries.Role;

public class GetRoleQuery : IRequest<Domain.Entities.Role>
{
    [Required] public string RoleId { get; init; } = null!;
}