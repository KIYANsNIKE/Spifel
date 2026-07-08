using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Spifel.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Spifel.Infra.Data.Configurations.UserConfig
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Key
            builder.HasKey(u => u.id);
            #endregion 

            #region Validations
            //Required
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(400);
            //END Required
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Mobile).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
            //NullAble
            builder.Property(u => u.FirstName).HasMaxLength(200);
            builder.Property(u => u.LastName).HasMaxLength(200);
            builder.Property(u => u.EmailActiveCode).HasMaxLength(50);

            //END NullAble
            #endregion 

            #region Relations
            builder.HasMany(u => u.UserAddresses)
               .WithOne(ua => ua.User);

            #endregion
        }
    }
}
