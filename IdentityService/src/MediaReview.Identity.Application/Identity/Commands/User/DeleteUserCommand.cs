﻿using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class DeleteUserCommand : IRequest<Unit>
{
    public string UserId { get; set; }
}