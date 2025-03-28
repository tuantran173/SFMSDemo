using SFMSSolution.Domain.Entities;
using System.ComponentModel;

namespace SFMSSolution.Domain.Enums
{
    public abstract class Roles
    {
        public const string Administrator = nameof(Administrator);
        public const string User = nameof(User);
        public const string FacilityOwner = nameof(FacilityOwner);
    }
}
