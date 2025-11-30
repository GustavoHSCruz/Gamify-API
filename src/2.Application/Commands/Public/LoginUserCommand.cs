using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Events.Public;
using Domain.Core.Requests.Public;
using Domain.Core.Responses.Public;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Public;

public class LoginUserCommand : Command<User, LoginUserRequest, LoginUserResponse, UserLoggedEvent>
{
    private User _user;
    private readonly IJwtService _jwtService;

    public LoginUserCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper, IJwtService jwtService) : base(mediator, repository, uow, mapper)
    {
        _jwtService = jwtService;
    }

    protected override async Task BeforeChanges(LoginUserRequest request)
    {
        _user = await _repository.SingleAsync<User>(x => x.Email == request.Email && x.IsActive && !x.IsDeleted, x=> x.Person);

        if (_user == null)
        {
            _response.AddError(EErrorCode.EmailOrPasswordWrong);
            
            return;
        }

        if (!PasswordService.VerifyPassword(request.Password, _user.Password))
        {
            _response.AddError(EErrorCode.EmailOrPasswordWrong);
            
            return;
        }
    }

    protected override Task<User> Changes(LoginUserRequest request)
    {
        var refreshToken = _jwtService.GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        return Task.FromResult(_user);
    }

    protected override async Task AfterChanges(User entity)
    {
        _response.Token = _jwtService.GenerateToken(_user);
        _response.RefreshToken = entity.RefreshToken!;
    }
}