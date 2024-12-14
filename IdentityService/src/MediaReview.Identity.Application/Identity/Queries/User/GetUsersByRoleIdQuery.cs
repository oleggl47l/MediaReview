using MediaReview.Identity.Domain.Models;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries.User;

public class GetUsersByRoleIdQuery : IRequest<IEnumerable<UserModel>>
{
    public string RoleId { get; set; }
}