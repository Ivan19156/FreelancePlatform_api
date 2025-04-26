using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Date
{
    public class FreelancePlatformDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Chat> Chats { get; set; }

        // Конструктор для передачі налаштувань
        public FreelancePlatformDbContext(DbContextOptions<FreelancePlatformDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string conn = "Server=localhost;Database=FreelancePlatformDB;Trusted_Connection=True;TrustServerCertificate=True;";
        //        optionsBuilder.UseSqlServer(conn);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування зв'язків між сутностями
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Executor)
                .WithMany(u => u.ExecutedProjects)
                .HasForeignKey(p => p.ExecutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Freelancer)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Requests)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Sender)
                .WithMany(u => u.SentFeedbacks)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Recipient)
                .WithMany(u => u.ReceivedFeedbacks)
                .HasForeignKey(f => f.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(c => c.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
