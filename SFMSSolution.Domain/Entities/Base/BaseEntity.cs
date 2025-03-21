using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; } // 🔥 ID của người tạo (nullable)
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; } // 🔥 ID của người tạo (nullable)
    }
}
