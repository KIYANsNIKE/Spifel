namespace Spifel.Web.Feature.Account.Login
{
    public class LoginVM
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsUserNotActive { get; set; } = false;
        public string ImageData { get; set; }
        public string CaptchaAnswer { get; set; }
    }
}
