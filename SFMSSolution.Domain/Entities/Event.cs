using SFMSSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Event: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        // Trạng thái của event, ví dụ: "Scheduled", "Ongoing", "Completed", "Cancelled"
        public string Status { get; set; } = "Scheduled";
    }
}
