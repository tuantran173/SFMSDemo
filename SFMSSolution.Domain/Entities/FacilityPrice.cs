using SFMSSolution.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SFMSSolution.Domain.Entities
{
    public class FacilityPrice : BaseEntity
    {
        public Guid FacilityTimeSlotId { get; set; }
        public FacilityTimeSlot FacilityTimeSlot { get; set; }
        public Guid FacilityId { get; set; }                    // 🔥 Thêm FK đến Facility
        public Facility Facility { get; set; }
        public decimal Coefficient { get; set; }              // Hệ số nhân giá
        public decimal BasePrice { get; set; }                // Giá cơ bản tại thời điểm tạo
        public decimal FinalPrice { get; set; }               // Giá sau khi áp dụng hệ số

        //public string FacilityType { get; set; } = string.Empty;  // Ghi lại loại sân tại thời điểm tạo
    }
}
