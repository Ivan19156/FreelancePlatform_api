namespace DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // freelancer or customer
        public decimal Balance { get; set; }

        public int Rank { get; set; }

        public ICollection<Project> OwnedProjects { get; set; }
        public ICollection<Project> ExecutedProjects { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Feedback> SentFeedbacks { get; set; }
        public ICollection<Feedback> ReceivedFeedbacks { get; set; }
        public ICollection<Chat> SentMessages { get; set; }
        public ICollection<Chat> ReceivedMessages { get; set; }
    }
}

