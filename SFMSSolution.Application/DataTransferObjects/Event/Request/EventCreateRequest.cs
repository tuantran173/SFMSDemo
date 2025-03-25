using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Event.Request
{
    public class EventCreateRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Images { get; set; }     
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
