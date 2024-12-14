using MediaReview.Identity.Domain.Entities;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Queries;

public class LoginQueryHandler(SignInManager<Domain.Entities.User> signInManager,
    UserManager<Domain.Entities.User> userManager) : IRequestHandler<LoginQuery, LoginModel>
{
    public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = new LoginModel();
        var user = await userManager.FindByNameAsync(request.UserName) ?? throw new NullReferenceException();
        var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
        
        if (signInResult.Succeeded)
        {
            result.User = new UserModel
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