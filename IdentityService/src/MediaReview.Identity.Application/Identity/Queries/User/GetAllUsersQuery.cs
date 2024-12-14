using MediaReview.Identity.Domain.Models;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries.User;

public class GetAllUsersQuery: IRequest<IEnumerable<UserModel>>;