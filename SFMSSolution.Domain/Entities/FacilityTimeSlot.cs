using SFMSSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class FacilityTimeSlot: BaseEntity
    {
        public Guid FacilityId { get; set; }  // FK đến Facility
        public Facility Facility { get; set; }

        public TimeSpan StartTime { get; set; }  // Giờ bắt đầu
        public TimeSpan EndTime { get; set; }  // Giờ kết thúc

        public DateTime StartDate { get; set; }  // Ngày bắt đầu áp dụng
        public DateTime EndDate { get; set; }    // Ngày kết thúc áp dụng
 
        public bool IsWeekend { get; set; }  // Có phải cuối tuần không (true = cuối tuần)

    }
}
