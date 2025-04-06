using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;

public class Booking : BaseEntity
{
    public DateTime BookingDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }

    public Guid FacilityTimeSlotId { get; set; }
    public FacilityTimeSlot FacilityTimeSlot { get; set; }

    public Guid FacilityId { get; set; }
    public Facility Facility { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Note { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public string PaymentMethod { get; set; } // "Cash", "VNPay", etc.
    public decimal FinalPrice { get; set; }

    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
}
