using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime purchaseDateTime   { get; set; }
        public Guid purchaseNumber  { get; set; }
    }
}
