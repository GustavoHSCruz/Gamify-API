using Bogus;
using Domain.Core.Entities;
using Domain.Core.Requests.Public;
using Test.Common;
using Test.Fakes;
using Test.Helpers;

namespace Test.Integration.Public;

public class RegisterUserTest : IntegrationTestCommand
{
    private string Email => _faker.Person.Email;
    private string Password => "Pwd@1234"; //Just to sure to follow a pattern
    private string FirstName => _faker.Person.FirstName;
    private string LastName => _faker.Person.LastName;

    [Fact(DisplayName = "User can register successfully")]
    public async Task User_Valid_Registration()
    {
        var request = new RegisterUserRequest
        {
            Email = Email,
            Password = Password,
            ConfirmPassword = Password,
            FirstName = FirstName,
            LastName = LastName,
            ReceiveNewsletter = _faker.Random.Bool()
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.NotNull(_writeContext.Set<User>().First().RefreshToken);
        Assert.NotEmpty(response.Token);
        Assert.NotEmpty(response.RefreshToken);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "User can register with database already populated")]
    public async Task User_Register_With_Database_Populated()
    {
        var user = new UserFake(Email, Password, Password).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);

        //Due to Faker Seed, I have to instantiate a new Faker
        var faker = new Faker();

        var request = new RegisterUserRequest
        {
            Email = faker.Person.Email,
            Password = Password,
            ConfirmPassword = Password,
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            ReceiveNewsletter = faker.Random.Bool()
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(2, _writeContext.Set<User>().Count());
        Assert.NotNull(_writeContext.Set<User>().First(x => x.RefreshToken == response.RefreshToken).RefreshToken);
        Assert.NotEmpty(response.Token);
        Assert.NotEmpty(response.RefreshToken);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "User Can't Register with same email")]
    public async Task Register_Same_Email()
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPwd = BCrypt.Net.BCrypt.HashPassword(Password, salt);

        var user = new UserFake(Email, hashedPwd, salt).Generate();

        await ContextHelpers.RegisterAsync(_repository, _uow, user);

        var request = new RegisterUserRequest
        {
            Email = Email,
            Password = Password,
            ConfirmPassword = Password,
            FirstName = FirstName,
            LastName = LastName,
            ReceiveNewsletter = _faker.Random.Bool()
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Email")), $"Expected error 'Email', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Register with no input")]
    public async Task Register_With_No_Input()
    {
        var request = new RegisterUserRequest();

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(0, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Email")), $"Expected error 'Email', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
    
    [Fact(DisplayName = "Register with invalid email")]
    public async Task Register_With_Invalid_Email()
    {
        var request = new RegisterUserRequest
        {
            Email = "invalid@email",
            Password = Password,
            ConfirmPassword = Password,
            FirstName = FirstName,
            LastName = LastName,
            ReceiveNewsletter = _faker.Random.Bool()
        };
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(0, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Email")), $"Expected error 'Email', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
    
    [Fact(DisplayName = "Register with invalid password")]
    public async Task Register_With_Invalid_Password()
    {
        var request = new RegisterUserRequest
        {
            Email = Email,
            Password = "password123",
            ConfirmPassword = "password123",
            FirstName = FirstName,
            LastName = LastName,
            ReceiveNewsletter = _faker.Random.Bool()
        };
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(0, _writeContext.Set<User>().Count());
        Assert.Null(response.Token);
        Assert.Null(response.RefreshToken);
        Assert.True(response.Errors.Any(x => x.Contains("Password")), $"Expected error 'Password', but found: {string.Join(", ", response.Errors)}");
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
}