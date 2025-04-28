using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Feedbacks
{
    public class AverageRatingDto
    {
        public int UserId { get; set; }
        public double AverageRating { get; set; }
    }

}
