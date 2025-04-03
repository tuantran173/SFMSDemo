using SFMSSolution.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request
{
    public class FacilityPriceUpdateRequestDto
    {
        public Guid Id { get; set; }                    // ID của giá
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan StartTime { get; set; } // Format: "HH:mm"
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan EndTime { get; set; }             // Giờ kết thúc
        public DateTime StartDate { get; set; }                  // Ngày bắt đầu áp dụng giá
        public DateTime EndDate { get; set; }                    // Ngày kết thúc áp dụng giá
        public decimal BasePrice { get; set; }                   // Giá gốc
        public decimal Coefficient { get; set; }
    }
}
