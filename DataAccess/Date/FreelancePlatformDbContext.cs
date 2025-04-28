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
                .HasOne(t => t.Sender)  // Зв'язок з відправником
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receiver)  // Зв'язок з отримувачем
                .WithMany()
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // Визначення точності та масштабу для полів типу decimal
            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Request>()
                .Property(r => r.OfferedPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");

       //     modelBuilder.Entity<User>().HasData(

       //    new User
       //    {
       //        Id = 3,
       //        Name = "Mike",

       //        Email = "mikejohnson@example.com",
       //        Password = "12313123",
       //        Role = "Customer",
       //        Balance = 2000,
       //        Rank = 1,
       //    }
       //);
            // Сідінг даних для таблиць
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    SenderId = 1,
                    ReceiverId = 2,
                    Amount = 1000m,
                    Type = "Payment",
                    Date = new DateTime(2025, 4, 27),  // Статична дата
                    Description = "Initial payment for project"
                },
                new Transaction
                {
                    Id = 2,
                    SenderId = 2,
                    ReceiverId = 1,
                    Amount = 500m,
                    Type = "Refund",
                    Date = new DateTime(2025, 4, 27).AddMinutes(10),  // Статична дата
                    Description = "Refund for the project"
                }
            );
        //    modelBuilder.Entity<Project>().HasData(
        //    new Project
        //    {
        //        Id = 1,
        //        Name = "Website Development",
        //        Description = "Create a responsive website for a small business.",
        //        Budget = 1500.00m,
        //        Status = "Open",  // статус проекту
        //        Category = "Web Development",  // категорія проекту
        //        CustomerId = 1, // потрібно передати реальний CustomerId з таблиці User
        //        ExecutorId = 2, // потрібно передати реальний ExecutorId з таблиці User
        //    },
        //    new Project
        //    {
        //        Id = 2,
        //        Name = "Mobile App Design",
        //        Description = "Design UI/UX for a new mobile application.",
        //        Budget = 2000.00m,
        //        Status = "Open",
        //        Category = "Mobile App Development",
        //        CustomerId = 1,
        //        ExecutorId = 3, // потрібно передати реальний ExecutorId з таблиці User
        //    },
        //    new Project
        //    {
        //        Id = 3,
        //        Name = "E-commerce Platform",
        //        Description = "Build an e-commerce platform with secure payment methods.",
        //        Budget = 5000.00m,
        //        Status = "In Progress",  // інший статус
        //        Category = "E-commerce",
        //        CustomerId = 2, // передати реальний CustomerId
        //        ExecutorId = 3, // передати реальний ExecutorId
        //    }
        //);
            // Додатковий сідінг для інших таблиць (Requests, Feedback, Chats) можна додавати аналогічно
        }
    }
}


