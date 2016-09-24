using System;
using System.Collections.Generic;
using EGS.Stark.Model.ACL;
using EGS.Stark.Model.Booking;
using EGS.Stark.Model.Pricing;

namespace EGS.Stark.Booking.Model
{
    public class BookItem:IBookItem
    {
        public int BookId { get; set; }
        public int ServiceId { get; set; }
        public int BookStatus { get; set; }
        public int Count { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalCostAmount { get; set; }

        #region Navigation
        public IBookStatus Status { get; set; }
        public IService Service { get; set; }
        public IEnumerable<IProfile> Passengers { get; set; }
        #endregion

        #region BaseEntity
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        #endregion
    }
}
