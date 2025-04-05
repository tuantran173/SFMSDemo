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
        public DateTime Date { get; set; }
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan StartTime { get; set; }
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan EndTime { get; set; }

        public string? Note { get; set; }
        public decimal? FinalPrice { get; set; }
        public SlotStatus? Status { get; set; }
    }
}
