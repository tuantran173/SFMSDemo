using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid FacilityId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
