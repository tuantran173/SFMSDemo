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
        public decimal BasePrice { get; set; } // Giá cơ bản
        public string FacilityType { get; set; } = string.Empty;
    }
}
