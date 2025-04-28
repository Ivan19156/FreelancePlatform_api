namespace FreelancePlatform.Core.DTOs.Transactions
{
    public class CreateTransactionDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
