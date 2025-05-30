﻿using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking
{
    public class SlotDetailDto
    {
        public Guid SlotId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SlotStatus Status { get; set; }

        public string Note { get; set; }

        public decimal BasePrice { get; set; }
        public decimal Coefficient { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Deposit { get; set; }
        public string? PriceImageUrl { get; set; }


        // ✅ Thông tin sân
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string FacilityImageUrl { get; set; }

        // ✅ Thông tin chủ sân
        public string OwnerFullName { get; set; }
        public string OwnerUserName { get; set; }
        public string OwnerPhone { get; set; }
    }
}
