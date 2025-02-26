using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Role: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string RoleCode { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
