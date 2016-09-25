using EGS.Stark.Booking.Manager;
using EGS.Stark.Booking.Menager;
using EGS.Stark.Booking.Model;
using EGS.Stark.Data;
using EGS.Stark.DependecyInjection;
using Microsoft.Practices.Unity;

namespace EGS.Stark.Booking.Injection
{
    public static class BookingInjection
    {
        public static IInjectionContainer RegisterContext(this IInjectionContainer container)
        {
            container.Container.RegisterType<IUoW, DataBaseContext>();
            return container;
        }
        public static IInjectionContainer RegisterServices(this IInjectionContainer container)
        {
            container.Container.RegisterType<IBookManager, BookManager>()
                .RegisterType<IRepository<Book>, Repository<Book>>()

                .RegisterType<IBookItemManager, BookItemManager>()
                .RegisterType<IRepository<BookItem>, Repository<BookItem>>();

            return container;
        }
    }
}
