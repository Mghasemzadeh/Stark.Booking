using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EGS.Stark.Booking.Manager;
using EGS.Stark.Booking.Model;
using EGS.Stark.Data;

namespace EGS.Stark.Booking.Menager
{
    public class BookItemService : IBookItemService
    {
        private readonly IRepository<BookItem> _bookItemRepository;

        public BookItemService(IRepository<BookItem> bookItemRepository)
        {
            _bookItemRepository = bookItemRepository;
        }

        public async Task<BookItem> AddAsync(BookItem bookItem)
        {
            _bookItemRepository.Add(bookItem);
            await _bookItemRepository.SaveAsync();
            return bookItem;
        }

        public async Task<BookItem> UpdateAsync(BookItem bookItem)
        {
            await _bookItemRepository.SaveAsync();
            return bookItem;
        }

        public async Task DeleteAsync(int id)
        {
            var merchant = _bookItemRepository.GetById(id);
            _bookItemRepository.Delete(merchant);
            await _bookItemRepository.SaveAsync();
        }

        public async Task<BookItem> GetById(int id)
        {
            return await _bookItemRepository.GetAsyncById(id);
        }

        public async Task<BookItem> GetAsyncById(int id)
        {
            return await _bookItemRepository.GetAsyncById(id);
        }

        public IQueryable<BookItem> GetAll()
        {
            return _bookItemRepository.GetAll();
        }

        public async Task<IEnumerable<BookItem>> GetAllAsync()
        {
            return await _bookItemRepository.GetAllAsync();
        }

        public async Task<BookItem> GetAsync(Expression<Func<BookItem, bool>> predicate)
        {
            return await _bookItemRepository.Get(predicate);
        }

        public IEnumerable<BookItem> GetAll(Expression<Func<BookItem, bool>> predicate, int start, int count)
        {
            return _bookItemRepository.GetAll(predicate, start, count);
        }

        public async Task<IEnumerable<BookItem>> GetAllAsync(Expression<Func<BookItem, bool>> predicate, int start, int count)
        {
            return await _bookItemRepository.GetAllAsync(predicate, start, count);
        }

        public IQueryable<BookItem> GetAllFiltering(Func<BookItem, bool> predicate)
        {
            return _bookItemRepository.GetAllFiltering(predicate);
        }
    }
}
