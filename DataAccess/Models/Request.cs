namespace DataAccess.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int FreelancerId { get; set; }
        public User Freelancer { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public decimal OfferedPrice { get; set; }
        public DateTime StartDate { get; set; }
    }
}

