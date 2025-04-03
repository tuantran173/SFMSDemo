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
        public string StartTime { get; set; } = string.Empty; // Format: "HH:mm"
        public string EndTime { get; set; } = string.Empty;                // Giờ kết thúc
        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateTime StartDate { get; set; }                  // Ngày bắt đầu áp dụng giá
        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateTime EndDate { get; set; }                    // Ngày kết thúc áp dụng giá
        public decimal BasePrice { get; set; }                   // Giá gốc
        public decimal Coefficient { get; set; }
    }
}
