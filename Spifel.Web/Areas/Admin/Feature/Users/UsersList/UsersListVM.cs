using Spifel.Domain.Models.Role;

namespace Spifel.Web.Areas.Admin.Feature.Users.UsersList
{
    public class UsersListVM
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string? Avatar { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public List<Role>? Roles { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }



    }
}
