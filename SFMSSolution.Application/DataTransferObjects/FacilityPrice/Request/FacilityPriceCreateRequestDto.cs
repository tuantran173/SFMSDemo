using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request
{
    public class FacilityPriceCreateRequestDto
    {
        public Guid FacilityId { get; set; }                    // ID của sân
        public string StartTime { get; set; } = string.Empty; // Format: "HH:mm"
        public string EndTime { get; set; } = string.Empty;                // Giờ kết thúc
        public DateTime StartDate { get; set; }                  // Ngày bắt đầu áp dụng giá
        public DateTime EndDate { get; set; }                    // Ngày kết thúc áp dụng giá
        public decimal BasePrice { get; set; }                   // Giá gốc
        public decimal Coefficient { get; set; }
    }
}
