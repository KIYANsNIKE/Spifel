using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Infra.Data.Configurations.UserConfig
{
    internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            #region Key
            builder.HasKey(u => u.id);
            #endregion

            #region Validations
            //Required

            //END Required

            //NullAble

            //END NullAble
            #endregion

            #region Relations
            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId);
            #endregion
        }
    }
}
