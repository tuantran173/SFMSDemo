using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Facility: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty; // => nên là url map
        public string Description { get; set; } = string.Empty;
        public string Capacity { get; set; } = string.Empty; // => int
        public string Images { get; set; } = string.Empty;

        // Thêm liên kết đến Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<FacilityTimeSlot> FacilityTimeSlots { get; set; } = new List<FacilityTimeSlot>();
    }

}
