using Spifel.Application.Services.Implementations.Features.Register;

namespace Spifel.Web.Feature.Account.Register
{
    public static class MappRegisterVM_ToRegisterDto
    {
        public static RegisterDto ToRegisterDto(this RegisterVM VM)
        {
            return new() { Email = VM.Email, Password = VM.Password, UserName = VM.UserName };
        }
    }
}
