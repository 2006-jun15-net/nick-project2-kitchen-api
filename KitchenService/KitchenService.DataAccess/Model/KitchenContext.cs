using System;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.DataAccess.Model
{
    public class KitchenContext : DbContext
    {
        public KitchenContext(DbContextOptions<KitchenContext> options)
            : base(options)
        {
        }

        public DbSet<FridgeItem> FridgeItems { get; set; }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FridgeItem>()
                .Property(n => n.Name)
                .IsRequired();

            modelBuilder.Entity<FridgeItem>().HasData(
                new FridgeItem { Id = 1, Name = "cheese", ExpirationDate = DateTimeOffset.Parse("2020-06-14") },
                new FridgeItem { Id = 2, Name = "steak", ExpirationDate = DateTimeOffset.Parse("2020-07-28") },
                new FridgeItem { Id = 3, Name = "salmon", ExpirationDate = DateTimeOffset.Parse("2020-07-28") });
        }
    }
}
