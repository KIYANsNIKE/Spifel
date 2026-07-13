using Spifel.Application.Services.Implementations.Features.ChangePassword;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.ChangePassword
{
    public static class MappChangePasswordVM_ToChangePasswordDto
    {
        public static ChangePasswordDto ToChangePasswordDto(this ChangePasswordVM VM, int UserId)
        {
            return new() { UserId = UserId, OldPassword = VM.OldPassword, NewPassword = VM.RePassword };
        }
    }
}
