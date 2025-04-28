using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Feedbacks
{
    public class LeaveFeedbackDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

}
