using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Core.Responses.Public;
using Domain.Shared.Requests;

namespace Domain.Core.Requests.Public;

public class RegisterUserRequest : CommandRequest<RegisterUserResponse>
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

    /// <summary>
    /// Repeat Password - Only allow request if both passwords are equal
    /// </summary>
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// User First Name
    /// </summary>
    [Required(ErrorMessage = "First name is required ")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    /// <summary>
    /// User Last Name
    /// </summary>
    [Required(ErrorMessage = "Last name is required ")]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    /// <summary>
    /// Option to receive newsletter - Default is true
    /// </summary>
    [DefaultValue(true)]
    public bool ReceiveNewsletter { get; set; }
}