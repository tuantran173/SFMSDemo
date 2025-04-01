using System;

namespace SFMSSolution.Application.DataTransferObjects.Facility
{
    public class FacilityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FacilityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid OwnerId { get; set; }
    }
}
