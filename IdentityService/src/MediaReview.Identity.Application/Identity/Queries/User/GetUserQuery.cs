using MediaReview.Identity.Domain.Models;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries.User;

public class GetUserQuery : IRequest<UserModel>
{
    public string UserId { get; set; }
}