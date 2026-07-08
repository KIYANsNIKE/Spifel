using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Models.Common
{
    public class BaseEntity
    {
        public int id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
