using SFMSSolution.Domain.Entities;
using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public enum Role
    {
        [Description("Administrator")]
        Admin,

        [Description("Facility Owner")]
        Owner,

        [Description("Customer")]
        Customer
    }
}
