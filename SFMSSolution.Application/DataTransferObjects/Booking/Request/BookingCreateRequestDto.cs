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
        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        // UserId có thể lấy từ token nếu cần, nhưng để cho đơn giản ta yêu cầu client truyền vào
        [Required]
        public Guid UserId { get; set; }
    }
}
