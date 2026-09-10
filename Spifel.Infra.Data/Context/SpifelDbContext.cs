using Microsoft.EntityFrameworkCore;
using Spifel.Domain.Models.Role;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Infra.Data.Context
{
    public class SpifelDbContext(DbContextOptions<SpifelDbContext> options) : DbContext(options)
    {
        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UsersAddress { get; set; }
        #endregion

        #region Roles 
        public DbSet<Role> Roles{ get; set; }
        public DbSet<UserInRoles> UserInRoles { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
