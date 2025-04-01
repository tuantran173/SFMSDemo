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

        public string Note { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Guid FacilityId { get; set; }
        public string FacilityName { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
