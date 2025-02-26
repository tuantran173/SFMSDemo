using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public enum BookingStatus
    {
        [Description("Pending Confirmation")]
        Pending = 1,

        [Description("Confirmed")]
        Confirmed = 2,

        [Description("Canceled")]
        Canceled = 3,

        [Description("Completed")]
        Completed = 4
    }
}
