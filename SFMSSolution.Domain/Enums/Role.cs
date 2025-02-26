using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public enum Role
    {
        [Description("Administrator")]
        Admin = 1,

        [Description("Facility Owner")]
        FacilityOwner = 2,

        [Description("Customer")]
        Customer = 3
    }
}
