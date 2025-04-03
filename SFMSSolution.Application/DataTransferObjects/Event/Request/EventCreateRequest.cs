using SFMSSolution.Application.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Event.Request
{
    public class EventCreateRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [Required]
        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateTime EndTime { get; set; }

        [Required]
        public string EventType { get; set; } = string.Empty;


        public string Status { get; set; } = "Scheduled";
    }
}
