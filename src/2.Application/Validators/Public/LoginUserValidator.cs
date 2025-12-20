using Domain.Core.Requests.Public;
using Domain.Shared.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Validators.Public;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Matches(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$").WithMessage(EErrorCode.EmailPatternNotMatch.ToString());
        RuleFor(x => x.Password).NotEmpty();
    }
}