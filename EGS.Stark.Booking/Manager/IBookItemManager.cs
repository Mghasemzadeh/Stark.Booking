using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EGS.Stark.Booking.Model;

namespace EGS.Stark.Booking.Manager
{
    public interface IBookItemManager
    {
        IQueryable<BookItem> GetAllFiltering(Func<BookItem, bool> predicate);
        Task<BookItem> AddAsync(BookItem bookItem);
        Task<BookItem> UpdateAsync(BookItem bookItem);
        Task DeleteAsync(int id);
        Task<BookItem> GetAsyncById(int id);
        Task<BookItem> GetAsync(Expression<Func<BookItem, bool>> predicate);
        Task<IEnumerable<BookItem>> GetAllAsync();
        IQueryable<BookItem> GetAll();
        IEnumerable<BookItem> GetAll(Expression<Func<BookItem, bool>> predicate, int start = 0, int count = 25);
        Task<IEnumerable<BookItem>> GetAllAsync(Expression<Func<BookItem, bool>> predicate, int start = 0, int count = 25);
    }
}
