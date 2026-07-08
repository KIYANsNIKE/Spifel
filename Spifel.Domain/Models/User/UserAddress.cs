using Spifel.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Models.User
{
    public class UserAddress : BaseEntity
    {
        #region Properties
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string PostalCode { get; set; }
        public required string Address { get; set; }
        #endregion

        #region Realations
        //Navigation property
        public User? User { get; set; }
        #endregion
    }
}
