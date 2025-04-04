using SFMSSolution.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.FacilityPrice
{
    public class FacilityPriceDto
    {
        public Guid Id { get; set; }

        // Thông tin sân
        public Guid FacilityId { get; set; }
        public string FacilityName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        // Thông tin timeslot
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan StartTime { get; set; }
        [JsonConverter(typeof(TimeSpanToStringConverter))]
        public TimeSpan EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // Thông tin giá
        public decimal BasePrice { get; set; }
        public decimal Coefficient { get; set; }
        public decimal FinalPrice { get; set; }


    }
    }
