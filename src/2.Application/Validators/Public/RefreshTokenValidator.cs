using Domain.Core.Requests.Public;
using FluentValidation;

namespace Application.Validators.Public;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
    }
}