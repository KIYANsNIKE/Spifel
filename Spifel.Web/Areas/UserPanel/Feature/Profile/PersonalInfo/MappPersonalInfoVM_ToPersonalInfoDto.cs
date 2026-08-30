using Spifel.Application.Services.Implementations.Features.ChangePassword;
using Spifel.Application.Services.Implementations.Features.Update;
using Spifel.Domain.Models.User;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo
{
    public static class MappPersonalInfoVM_ToUser
    {
        public static UpdateDto ToUpdateDto(this PersonalInfoVM VM,int userId)
        {
            return new UpdateDto
            {
                Id = userId,
                UserName = VM.UserName,
                Name = VM.Name,
                LastName = VM.LastName,
                Email = VM.Email,
                PhoneNumber = VM.PhoneNumber,
                BirthDate = VM.BirthDate,
                NationalCode = VM.NationalCode,
                //Password = VM.Password,
            };
        }
    }
}

