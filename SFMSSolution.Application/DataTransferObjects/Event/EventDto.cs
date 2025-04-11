using SFMSSolution.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }= string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid OwnerId { get; set; }
    }
}
