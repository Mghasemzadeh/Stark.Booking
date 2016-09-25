using EGS.Stark.Booking.Model;
using EGS.Stark.Data;

namespace EGS.Stark.Booking
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataBaseContext : DbContext, IUoW
    {
        public DataBaseContext()
            : base("name=DataBaseContext")
        {

        }
        public virtual DbSet<Book> Supplier { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
