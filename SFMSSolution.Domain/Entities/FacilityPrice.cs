using SFMSSolution.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SFMSSolution.Domain.Entities
{
    public class FacilityPrice : BaseEntity
    {
        public Guid FacilityTimeSlotId  { get; set; }
        public FacilityTimeSlot FacilityTimeSlot  { get; set; }
        public decimal Coefficient { get; set; }
        // Giá thuê cho khung giờ này
        public decimal FinalPrice { get; set; }
    }
}
