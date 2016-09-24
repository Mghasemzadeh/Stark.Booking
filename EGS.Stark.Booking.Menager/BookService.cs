using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EGS.Stark.Booking.Manager;
using EGS.Stark.Data;
using EGS.Stark.Booking.Model;

namespace EGS.Stark.Booking.Menager
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _bookRepository.Add(book);
            await _bookRepository.SaveAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            await _bookRepository.SaveAsync();
            return book;
        }

        public async Task DeleteAsync(int id)
        {
            var book = _bookRepository.GetById(id);
            _bookRepository.Delete(book);
            await _bookRepository.SaveAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepository.GetAsyncById(id);
        }

        public async Task<Book> GetAsyncById(int id)
        {
            return await _bookRepository.GetAsyncById(id);
        }

        public IQueryable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _bookRepository.Get(predicate);
        }

        public IEnumerable<Book> GetAll(Expression<Func<Book, bool>> predicate, int start, int count)
        {
            return _bookRepository.GetAll(predicate, start, count);
        }

        public async Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>> predicate, int start, int count)
        {
            return await _bookRepository.GetAllAsync(predicate, start, count);
        }

        public IQueryable<Book> GetAllFiltering(Func<Book, bool> predicate)
        {
            return _bookRepository.GetAllFiltering(predicate);
        }
    }
}
