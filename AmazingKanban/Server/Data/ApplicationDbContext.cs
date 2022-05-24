using AmazingKanban.Shared.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AmazingKanban.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardUserAccess> BoardUserAccesses { get; set; }
        public DbSet<KanbanTask<ApplicationUser>> KanbanTasks { get; set; }
        public DbSet<TaskComment<ApplicationUser>> TaskComments { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KanbanTask<ApplicationUser>>()
                .HasOne(k => k.CreatedBy).WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("CreatedById");

            modelBuilder.Entity<KanbanTask<ApplicationUser>>()
                .HasOne(k => k.AssignedTo).WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("AssignedToId");

            modelBuilder.Entity<KanbanTask<ApplicationUser>>()
                .HasOne(k => k.Validator).WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("ValidatorId");
        }
    }
}