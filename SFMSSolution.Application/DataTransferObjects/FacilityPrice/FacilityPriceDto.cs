using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.FacilityPrice
{
    public class FacilityPriceDto
    {
        public Guid Id { get; set; }

        public Guid FacilityTimeSlotId { get; set; } // Liên kết đến slot

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public decimal Coefficient { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
