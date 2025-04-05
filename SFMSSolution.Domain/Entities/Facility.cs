using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public string FacilityType { get; set; }
        public FacilityStatus Status { get; set; } = FacilityStatus.Available;
        // Chủ sân (Facility Owner)
        public Guid OwnerId { get; set; }                      // FK
        public User Owner { get; set; }                        // Navigation property

        public ICollection<FacilityTimeSlot> FacilityTimeSlots { get; set; } = new List<FacilityTimeSlot>();

    }

}
