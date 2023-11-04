namespace RegistrantApp.Shared.PresentationLayer.Security;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime DateTimeSessionStarted { get; set; }
    public DateTime DateTimeSessionExpired { get; set; }
}