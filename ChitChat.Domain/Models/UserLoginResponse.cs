
namespace ChitChat.Domain.Models;

public class UserLoginResponse
{
    public bool AuthenticateResult { get; set; }
    public string AuthToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpireDate { get; set; }
}
