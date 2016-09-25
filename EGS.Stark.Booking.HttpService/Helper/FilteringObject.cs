using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGS.Stark.Booking.HttpService.Helper
{
    public class FilteringObject
    {
        public int UserId { get; set; }
        public int MerchantId { get; set; }
        public int PurposeId { get; set; }
        public int StatusId { get; set; }
    }
}