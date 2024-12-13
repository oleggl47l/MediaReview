using MediaReview.Identity.Domain.Entities;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Queries;

public class LoginQueryHandler(SignInManager<User> signInManager,
    UserManager<User> userManager) : IRequestHandler<LoginQuery, LoginDto>
{
    public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = new LoginDto();
        var user = await userManager.FindByNameAsync(request.UserName) ?? throw new NullReferenceException();
        var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
        
        if (signInResult.Succeeded)
        {
            result.User = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
        else
        {
            throw new NullReferenceException("Invalid username or password");
        }
        
        return result;
    }
}