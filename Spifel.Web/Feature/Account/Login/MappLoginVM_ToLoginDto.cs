using Spifel.Application.Services.Implementations.Features.Login;

namespace Spifel.Web.Feature.Account.Login
{
    public static class MappLoginVM_ToLoginDto
    {
        public static LoginDto ToLoginDto(this LoginVM VM)
        {
            return new() { UserNameOrEmail = VM.UserNameOrEmail, Password = VM.Password, RememberMe = VM.RememberMe };
        }
    }
}
