namespace DataAccess.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public int Ratings { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}

