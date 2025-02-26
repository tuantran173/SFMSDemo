using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public enum FacilityStatus
    {
        [Description("Available")]
        Available = 1,

        [Description("Under Maintenance")]
        Maintenance = 2,

        [Description("Closed")]
        Closed = 3
    }
}
