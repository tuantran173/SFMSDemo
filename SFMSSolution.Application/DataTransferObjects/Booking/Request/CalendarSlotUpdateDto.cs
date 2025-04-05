using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class CalendarSlotUpdateDto
    {
        public Guid SlotId { get; set; }
        public decimal? FinalPrice { get; set; }
        public string? Description { get; set; }
        public SlotStatus? Status { get; set; }
    }

}
