using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class BookingSchedule: BaseEntity
    {
        [Required]
        public Guid FacilityId { get; set; }

        // Navigation property cho Facility
        public Facility Facility { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
        public FacilityStatus Status { get; set; } = FacilityStatus.Available;
    }
}
