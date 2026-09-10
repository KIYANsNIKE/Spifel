using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spifel.Domain.Models.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Infra.Data.Configurations.RoleConfig
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            #region Key
            builder.HasKey(u => u.id);
            #endregion

            #region Validations
            //Required
            builder.Property(r => r.id).IsRequired().HasMaxLength(200);
            //END Required

            //NullAble

            //END NullAble
            #endregion

            #region Relations
            builder.HasMany(r=>r.UserInRoles).WithOne(u=>u.Role)
                .HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
