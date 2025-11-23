using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Events.Public;
using Domain.Core.Requests.Public;
using Domain.Core.Responses.Public;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using MediatR;
using BCrypt.Net;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Application.Commands.Public;

public class RegisterUserCommand : Command<User, RegisterUserRequest, RegisterUserResponse, UserRegisteredEvent>
{
    private readonly IJwtService _jwtService;
    private string _token;
    private string _refreshToken;

    public RegisterUserCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper, IJwtService jwtService) : base(mediator, repository, uow, mapper)
    {
        _jwtService = jwtService;
    }

    protected override async Task BeforeChanges(RegisterUserRequest request)
    {
        var user = await _repository.SingleAsync<User>(x => x.Email == request.Email);

        if (user != null)
        {
            _response.AddError(EErrorCode.EmailAlreadyInUse);

            return;
        }
    }

    protected override Task<User> Changes(RegisterUserRequest request)
    {
        var person = new Person(request.FirstName, request.LastName);

        var passwordInfo = PasswordService.CryptPassword(request.Password);
        var user = new User(person, request.Email, passwordInfo.HashedPassword, passwordInfo.Salt);

        _token = _jwtService.GenerateToken(user);
        _refreshToken = _jwtService.GenerateRefreshToken();

        return Task.FromResult(user);
    }

    protected override async Task AfterChanges(User entity)
    {
        _response.Token = _token;
        _response.RefreshToken = _refreshToken;
    }
}