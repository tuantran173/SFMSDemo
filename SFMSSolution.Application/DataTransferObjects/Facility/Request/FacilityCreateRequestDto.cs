using SFMSSolution.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SFMSSolution.Application.DataTransferObjects.Facility.Request
{
    public class FacilityCreateRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string FacilityType { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
        public FacilityStatus Status { get; set; } = FacilityStatus.Available;
    }
}
