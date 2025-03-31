using SFMSSolution.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SFMSSolution.Application.DataTransferObjects.Facility.Request
{
    public class FacilityUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string FacilityType { get; set; } = string.Empty;
        public FacilityStatus Status { get; set; }
    }
}
