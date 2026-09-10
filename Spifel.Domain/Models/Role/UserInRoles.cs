using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Models.Role
{
    public class UserInRoles
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        #region Realations
        //Navigation property
        public User.User? User { get; set; }
        public Role? Role { get; set; }
        #endregion

    }
}
