using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Chats
{
    public class ChatDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public int FreelancerId { get; set; }

        //public string Message { get; set; }
    }

}
