using Spifel.Application.Services.Implementations.Features.ChangeAvatar;
using Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.ChangeAvatar
{
    public static class MappPersonalInfoVM_ToChangeAvatarDto
    {
        public static ChangeAvatarDto ToChangeAvatarDto(this PersonalInfoVM vm, int userId)
        {
            return new ChangeAvatarDto
            {
                UserId = userId,
                AvatarFile = vm.AvatarFile,
            };
        }
    }
}
