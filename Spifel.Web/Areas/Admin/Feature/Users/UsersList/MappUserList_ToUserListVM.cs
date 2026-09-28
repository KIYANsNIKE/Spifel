using Spifel.Domain.Models.User;

namespace Spifel.Web.Areas.Admin.Feature.Users.UsersList
{
    public static class MappUserList_ToUserListVM
    {
        public static IEnumerable<UsersListVM> ToUsersListVM(this IEnumerable<User> users)
        {
            return users.Select(user => new UsersListVM
            {
                Id = user.id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                //Password = user.Password,
                IsActive = user.IsActive,
                Avatar = user.Avatar,
                Roles = user.UserInRoles?
                    .Where(ur => ur.Role != null)
                    .Select(ur => ur.Role!)
                    .ToList() ?? [],
                CreateDate = user.CreateDate,
                ModifiedDate = user.ModifiedDate,
            });
            
        }
    }
}
