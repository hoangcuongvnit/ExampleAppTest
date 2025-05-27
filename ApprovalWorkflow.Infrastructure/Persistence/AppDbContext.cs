using ApprovalWorkflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApprovalWorkflow.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Approval> Approvals { get; set; }

        // Optional: configure entity mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Approval>().ToTable("approval");
            base.OnModelCreating(modelBuilder);
        }
    }
}
