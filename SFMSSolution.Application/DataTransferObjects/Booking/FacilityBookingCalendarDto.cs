using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking
{
    public class FacilityBookingCalendarDto
    {
        public Guid FacilityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;
        public List<FacilityBookingSlotDto> Calendar { get; set; } = new();
    }
}
