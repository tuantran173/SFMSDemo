using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class BookingCreateRequestDto
    {
        public DateTime BookingDate { get; set; }
        public Guid FacilityId { get; set; }
        public Guid FacilityTimeSlotId { get; set; }
        public string Note { get; set; } = string.Empty;
        public Guid UserId { get; set; }  // Có thể set từ Controller hoặc token
    }
}
