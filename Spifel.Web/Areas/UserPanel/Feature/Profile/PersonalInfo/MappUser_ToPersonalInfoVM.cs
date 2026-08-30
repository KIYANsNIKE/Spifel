using Spifel.Domain.Models.User;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo
{
    public static class MappUser_ToPersonalInfoVM
    {
        public static PersonalInfoVM ToPersonalInfoVM(this User user)
        {
            return new PersonalInfoVM()
            {
                Avatar = user.Avatar,
                Name = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.Mobile,
            };
        }
    }
}
