namespace DataAccess.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}

