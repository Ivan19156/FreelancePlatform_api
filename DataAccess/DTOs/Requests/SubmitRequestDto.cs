using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Requests
{
    public class SubmitRequestDto
    {
        public int FreelancerId { get; set; }
        public int ProjectId { get; set; }
        public decimal OfferedPrice { get; set; }
        public DateTime StartDate { get; set; }
    }


}
