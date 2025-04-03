using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class BookingUpdateRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public Guid FacilityTimeSlotId { get; set; }

        public string Note { get; set; } = string.Empty;

        // Trạng thái: Pending, Confirmed, Completed, Canceled...
        public string? Status { get; set; }
    }
}
