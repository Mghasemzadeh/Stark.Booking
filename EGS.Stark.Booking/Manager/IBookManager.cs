using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EGS.Stark.Booking.Model;

namespace EGS.Stark.Booking.Manager
{
    public interface IBookManager
    {
        IQueryable<Book> GetAllFiltering(Func<Book, bool> predicate);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<Book> GetAsyncById(int id);
        Task<Book> GetAsync(Expression<Func<Book, bool>> predicate);
        Task<IEnumerable<Book>> GetAllAsync();
        IQueryable<Book> GetAll();
        IEnumerable<Book> GetAll(Expression<Func<Book, bool>> predicate, int start = 0, int count = 25);
        Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>> predicate, int start = 0, int count = 25);
    }
}
