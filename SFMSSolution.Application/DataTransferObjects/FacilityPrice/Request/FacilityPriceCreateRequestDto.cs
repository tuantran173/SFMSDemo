using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request
{
    public class FacilityPriceCreateRequestDto
    {
        public Guid FacilityTimeSlotId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Coefficient { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsWeekend { get; set; }
    }
}
