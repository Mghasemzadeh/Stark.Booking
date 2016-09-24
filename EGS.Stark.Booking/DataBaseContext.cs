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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
