using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
