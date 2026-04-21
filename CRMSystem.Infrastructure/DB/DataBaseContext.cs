
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Comment;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace CRMSystem.Infrastructure.DB
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<TaskM> Tasks { get; set; } = null!;
        public DbSet<TaskComment> TaskComments { get; set; } = null!;
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Email)
                      .IsUnique();
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Email)
                      .IsUnique();
                entity.HasOne(c => c.CreatedBy)
                    .WithMany()
                    .HasForeignKey(c => c.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<TaskM>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(c => c.Customer)
                    .WithMany()
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.AssignedTo)
                    .WithMany()
                    .HasForeignKey(c => c.AssignedToId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.CreatedBy)
                    .WithMany()
                    .HasForeignKey(c => c.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<TaskComment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(c => c.Task)
                    .WithMany()
                    .HasForeignKey(c => c.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
