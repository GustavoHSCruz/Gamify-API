using Bogus;
using Domain.Core.Entities;
using Domain.Core.Requests.Public;
using Test.Common;
using Test.Fakes;
using Test.Helpers;

namespace Test.Integration.Public;

public class RefreshTokenTest : IntegrationTestCommand
{
    private string Email => _faker.Person.Email;
    private static string Password => "Pwd@1234"; //Just to sure to follow a pattern
    private static string Salt => BCrypt.Net.BCrypt.GenerateSalt();
    private static string HashedPwd => BCrypt.Net.BCrypt.HashPassword(Password, Salt);

    [Fact(DisplayName = "Refresh Token is valid")]
    public async Task Refresh_Token_Valid()
    {
        var refreshToken = _faker.Random.AlphaNumeric(100);

        var user = new UserFake(Email, HashedPwd, Salt).Generate();
        user.RefreshToken = refreshToken;

        await ContextHelpers.RegisterAsync(_repository, _uow, user);

        var request = new RefreshTokenRequest
        {
            Email = Email,
            RefreshToken = refreshToken
        };
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.NotEmpty(response.Token);
        Assert.NotEmpty(response.RefreshToken);
        Assert.NotEqual(refreshToken, response.RefreshToken);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Refresh Token with database populated")]
    public async Task Refresh_Token_With_Database_Populated()
    {
        var user1 = new UserFake(Email, HashedPwd, Salt).Generate();
        user1.RefreshToken = _faker.Random.AlphaNumeric(100);
        
        var user2 = new UserFake(new Faker().Person.Email, HashedPwd, Salt).Generate();
        user2.RefreshToken = _faker.Random.AlphaNumeric(100);

        var users = new List<User> { user1, user2 };
        
        await ContextHelpers.RegisterAsync(_repository, _uow, users);

        var email = _faker.Random.ListItem(users).Email;
        
        var request = new RefreshTokenRequest
        {
            Email = email,
            RefreshToken = users.Where(x => x.Email == email).First().RefreshToken
        };
        request.SetUserId(users.First(x => x.Email == email).Id);

        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(2, _writeContext.Set<User>().Count());
        Assert.NotEmpty(response.Token);
        Assert.NotEmpty(response.RefreshToken);
        Assert.NotEqual(request.RefreshToken, response.RefreshToken);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }
    
    [Fact(DisplayName = "Refresh Token with invalid refresh token")]
    public async Task Refresh_Token_With_Invalid_Refresh_Token()
    {
        var user = new UserFake(Email, HashedPwd, Salt).Generate();
        user.RefreshToken = _faker.Random.AlphaNumeric(100);
        
        await ContextHelpers.RegisterAsync(_repository, _uow, user);
        
        var request = new RefreshTokenRequest
        {
            Email = Email,
            RefreshToken = _faker.Random.AlphaNumeric(100)
        };
        request.SetUserId(user.Id);
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("RefreshToken")), $"Expected error 'Invalid refresh token', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
    
    [Fact(DisplayName = "Refresh Token with empty refresh token")]
    public async Task Refresh_Token_With_Empty_Refresh_Token()
    {
        var user = new UserFake(Email, HashedPwd, Salt).Generate();
        user.RefreshToken = _faker.Random.AlphaNumeric(100);
        
        await ContextHelpers.RegisterAsync(_repository, _uow, user);

        var request = new RefreshTokenRequest
        {
            Email = Email,
            RefreshToken = string.Empty
        };
        request.SetUserId(user.Id);
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Refresh Token")), $"Expected error 'Invalid refresh token', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
}