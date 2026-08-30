namespace Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo
{
    public class PersonalInfoVM
    {
        public string Avatar { get; set; }
        public IFormFile AvatarFile { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        //public string RefundMethod { get; set; }

    }
}
