using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGS.Stark.Model.Booking;
using EGS.Stark.Model.Content;

namespace EGS.Stark.Booking.Model
{
     public class TravelPurpose:ITravelPurpose
    {
        public string CotnentId { get; set; }
        public IDictionaryContent Content { get; set; }

        #region BaseEntity
        public byte Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        #endregion
    }
}
