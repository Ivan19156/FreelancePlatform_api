using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Chats
{
    public class UpdateChatDto
    {
        
        public int CustomerId { get; set; }
        public int FreelancerId { get; set; }
        public string Message { get; set; }
    }

}
