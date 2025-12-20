using Domain.Core.Requests.Public;
using Domain.Shared.Enums;
using FluentValidation;

namespace Application.Validators.Public;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Matches(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$").WithMessage(EErrorCode.EmailPatternNotMatch.ToString());
        RuleFor(x => x.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$").WithMessage(EErrorCode.PasswordPatternNotMatch.ToString());
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage(EErrorCode.PasswordAndConfirmPasswordMustBeEqual.ToString());
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }    
}