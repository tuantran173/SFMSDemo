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
        public decimal Coefficient { get; set; }
        public decimal BasePrice { get; set; }
        public string FacilityType { get; set; } = string.Empty;
    }
}
