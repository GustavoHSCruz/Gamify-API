using System.ComponentModel.DataAnnotations;
using Domain.Core.Responses.Public;
using Domain.Shared.Requests;

namespace Domain.Core.Requests.Public;

public class RefreshTokenRequest : CommandRequest<RefreshTokenResponse>
{
    /// <summary>
    /// User Email Address for refresh token
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    /// <summary>
    /// User Refresh Token
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required")]
    [DataType(DataType.Text)]
    public string RefreshToken { get; set; }
}