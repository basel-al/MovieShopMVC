using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> MovieReviews { get; set; }



    }
}
