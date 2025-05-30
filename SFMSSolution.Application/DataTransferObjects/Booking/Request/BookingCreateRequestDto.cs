﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class BookingCreateRequestDto
    {
        public Guid FacilityId { get; set; }
        public Guid FacilityTimeSlotId { get; set; }

        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        //public string OwnerFullName { get; set; }
        //public string OwnerPhone { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        public string? Note { get; set; }
        public decimal FinalPrice { get; set; }
        public string PayImageUrl { get; set; }

    }

}
