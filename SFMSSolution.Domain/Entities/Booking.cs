using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Booking: BaseEntity
    {
        // Ngày đặt (có thể là ngày làm việc)
        public DateTime BookingDate { get; set; }
        // Thời gian bắt đầu và kết thúc của booking
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Note { get; set; }
        // Khóa ngoại tới Facility và User (người đặt)
        public Guid FacilityId { get; set; }
        public Guid UserId { get; set; }

        // Trạng thái đặt chỗ
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        // Navigation properties
        public Facility Facility { get; set; }
        public User User { get; set; }
    }
}
