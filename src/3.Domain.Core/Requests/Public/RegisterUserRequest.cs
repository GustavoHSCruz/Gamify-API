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
    [Required(ErrorMessage =  "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    /// <summary>
    /// User Password
    /// </summary>
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    /// <summary>
    /// Repeat Password - Only allow request if password is equal
    /// </summary>
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
    /// Realiza uma transferência bancária entre contas.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Esta operação é irreversível. O saldo é debitado imediatamente da conta de origem.
    /// </para>
    /// <para>
    /// <b>Atenção:</b> Transações acima de R$ 10.000 precisam de aprovação manual do gerente.
    /// O tempo de processamento pode levar até <b>24 horas</b>.
    /// </para>
    /// </remarks>
    [DefaultValue(true)]
    public bool RecieveNewsletter { get; set; }
}