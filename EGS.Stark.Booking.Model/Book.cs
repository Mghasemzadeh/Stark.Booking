using System;
using System.Collections.Generic;
using EGS.Stark.Model.ACL;
using EGS.Stark.Model.Booking;
using EGS.Stark.Model.Pricing;
using EGS.Stark.Model.Provider;

namespace EGS.Stark.Booking.Model
{
    public class Book :IBook
    {
        public Book()
        {
            IsActive = true;
            CreatationDateTime=DateTime.Now;
        }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public int MerchantId { get; set; }
        public int PurposeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalCostAmount { get; set; }
        public int StatusId { get; set; }
        public string Phone { get; set; }
        public string ExtraRequest { get; set; }
        public DateTime ExpirationDateTime { get; set; }

        #region Nanigation
        public IUser User { get; set; }
        public IMerchant Merchant { get; set; }
        public ICurrency Currency { get; set; }
        public IBookStatus Status { get; set; }
        public ITravelPurpose Purpose { get; set; }
        public IEnumerable<IBookItem> Items { get; set; }
        #endregion

        #region BaseEntity
        public  int Id { get; set; }
        public  bool IsActive { get; set; }
        public  DateTime CreatationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public  DateTime? DeleteDateTime { get; set; }
        #endregion
    }
}
