using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class SlotDetail: BaseEntity
    {
        public Guid SlotId { get; set; }    
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public SlotStatus Status { get; set; }
        public decimal FinalPrice { get; set; }
        public string? Note { get; set; }

        public FacilityTimeSlot? FacilityTimeSlot { get; set; }
    }

}
