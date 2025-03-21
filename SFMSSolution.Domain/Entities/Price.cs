using SFMSSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class Price: BaseEntity
    {
        public Guid CategoryId { get; set; } // Khóa ngoại tham chiếu đến Category
        public decimal BasePrice { get; set; } // Giá cơ bản

        // Navigation Property
        public Category Category { get; set; }
    }
}
