using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking
{
    public class FacilityPriceDetailDto
    {
        public Guid SlotId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Coefficient { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
