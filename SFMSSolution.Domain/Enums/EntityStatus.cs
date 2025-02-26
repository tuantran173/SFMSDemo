using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public enum EntityStatus
    {
        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2,

        [Description("Deleted")]
        Deleted = 3
    }
}
