using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Assignment4.Entities
{
public class KanbanContext : DbContext
    {
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<User> User { get; set; }

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Task>(entity =>
            {
                entity.Property(e => e.State)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)Enum.Parse(typeof(State), v));
                entity.HasMany(e => e.Tags).WithMany(e => e.Tasks);
            });

            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasKey(u => u.UserID);
        }
    }
}
