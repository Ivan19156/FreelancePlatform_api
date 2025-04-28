using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Projects
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public int CustomerId { get; set; }
        public int ExecutorId { get; set; }
    }

}
