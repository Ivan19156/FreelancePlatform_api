namespace DataAccess.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int ExecutorId { get; set; }
        public User Executor { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}

