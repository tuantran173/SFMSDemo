using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Facility.Request
{
    public class FacilityUpdateRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public string Capacity { get; set; } = string.Empty;

        public string Images { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
