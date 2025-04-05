using SFMSSolution.Application.Extensions;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class UpdateSlotDetailRequestDto
    {
        public Guid SlotId { get; set; }

        public DateTime Date { get; set; }            // Ngày cụ thể
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan StartTime { get; set; }       // Bắt đầu slot nhỏ
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan EndTime { get; set; }         // Kết thúc slot nhỏ
        public Guid FacilityTimeSlotId { get; set; }  // Slot lớn

        public SlotStatus? Status { get; set; }
        public string? Note { get; set; }
        public decimal? FinalPrice { get; set; }
    }
}
