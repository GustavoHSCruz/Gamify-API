using System.ComponentModel.DataAnnotations;
using Domain.Core.Responses.Public;
using Domain.Shared.Requests;

namespace Domain.Core.Requests.Public;

public class LoginUserRequest : CommandRequest<LoginUserResponse>
{
    /// <summary>
    /// User Email Address
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    /// <summary>
    /// User Password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}