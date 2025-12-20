using Application.Common;
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

public class RefreshTokenCommand : Command<User, RefreshTokenRequest, RefreshTokenResponse, TokenRefreshedEvent>
{
    private User _user;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper, IJwtService jwtService) : base(mediator, repository, uow, mapper)
    {
        _jwtService = jwtService;
    }

    protected override async Task BeforeChanges(RefreshTokenRequest request)
    {
        _user = await _repository.SingleAsync<User>(x => x.Id == request.GetUserId() && x.Email == request.Email && x.RefreshToken == request.RefreshToken && x.IsActive && !x.IsDeleted);

        if (_user == null)
        {
            _response.AddError(EErrorCode.CannotUpdateRefreshToken);
            
            return;
        }
    }

    protected override Task<User> Changes(RefreshTokenRequest request)
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