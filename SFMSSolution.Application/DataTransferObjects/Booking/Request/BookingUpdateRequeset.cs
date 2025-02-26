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
        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Cho phép cập nhật trạng thái booking (ví dụ: "Confirmed", "Cancelled", "Completed")
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
