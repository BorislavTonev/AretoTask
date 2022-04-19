using AretoExercise.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AretoExercise.Data
{
    public class AretoDBContext: DbContext
    {
        public AretoDBContext(DbContextOptions<AretoDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<TransactionDbEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .HasIndex(p => new { p.Id, p.Username })
              .IsUnique(true);
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, FirstName = "Admin", LastName = "Developer", Username = "Admin", Password = "Admin" });
        }
    }
}
