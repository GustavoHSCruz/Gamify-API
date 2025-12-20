using Domain.Core.Entities;
using Domain.Core.Requests.Public;
using Domain.Shared.Enums;
using Test.Common;
using Test.Fakes;
using Test.Helpers;

namespace Test.Integration.Public;

public class LoginUserTest : IntegrationTestCommand
{
    private string Email => _faker.Person.Email;
    private string Password => "Pwd@1234"; //Just to sure to follow a pattern
    private string Salt => BCrypt.Net.BCrypt.GenerateSalt();
    private string HashedPwd => BCrypt.Net.BCrypt.HashPassword(Password, Salt);

    [Fact(DisplayName = "User can login successfully")]
    public async Task Login_Valid_User()
    {
        var user = new UserFake(Email, HashedPwd, Salt).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);

        var request = new LoginUserRequest
        {
            Email = Email,
            Password = Password
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.NotNull(_writeContext.Set<User>().First().RefreshToken);
        Assert.NotEmpty(response.Token);
        Assert.NotEmpty(response.RefreshToken);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }
    
    [Fact(DisplayName = "Login with no input")]
    public async Task Login_With_No_Input()
    {
        var user = new UserFake(Email, HashedPwd, Salt).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);
        
        var request = new LoginUserRequest();
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Email")), $"Expected error 'Email', but found: {string.Join(", ", response.Errors)}");
        Assert.True(response.Errors.Any(x => x.Contains("Password")), $"Expected error 'Password', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }

    [Fact(DisplayName = "User can't login with invalid credentials")]
    public async Task Login_Invalid_User()
    {
        var user = new UserFake(Email, HashedPwd, Salt).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);
        
        var request = new LoginUserRequest
        {
            Email = Email,
            Password = _faker.Internet.Password()
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Contains(EErrorCode.WrongEmailOrPassword.ToString()), $"Expected error '{EErrorCode.WrongEmailOrPassword}', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }

    [Fact(DisplayName = "User doesn't exist")]
    public async Task Login_User_Doesnt_Exist()
    {
        
        var user = new UserFake(Email, HashedPwd, Salt).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);
        
        var request = new LoginUserRequest
        {
            Email = "dontexist@email.com",
            Password = Password
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Contains(EErrorCode.WrongEmailOrPassword.ToString()), $"Expected error '{EErrorCode.WrongEmailOrPassword}', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
}