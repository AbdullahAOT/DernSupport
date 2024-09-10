using DernSupport2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important: Call the base method to configure Identity tables

            // Configure one-to-many relationship between ApplicationUser and SupportRequest
            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.User) // Updated to ApplicationUser
                .WithMany(u => u.SupportRequests) // Navigation property in ApplicationUser
                .HasForeignKey(sr => sr.UserId) // Updated foreign key
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship between SupportRequest and Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.SupportRequest)
                .WithMany(sr => sr.Feedbacks)
                .HasForeignKey(f => f.SupportRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship between SupportRequest and Job
            modelBuilder.Entity<Job>()
                .HasOne(j => j.SupportRequest)
                .WithMany(sr => sr.Jobs)
                .HasForeignKey(j => j.SupportRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
