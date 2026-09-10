using Spifel.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Models.Role
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        #region Realations
        //Navigation property
        public ICollection<UserInRoles>? UserInRoles { get; set; }
        #endregion
    }
}
