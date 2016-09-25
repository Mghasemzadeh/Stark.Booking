using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EGS.Stark.Booking.Model;

namespace EGS.Stark.Booking.Manager
{
    public interface ITravelPurposeManager
    {
        IQueryable<TravelPurpose> GetAllFiltering(Func<TravelPurpose, bool> predicate);
        Task<TravelPurpose> AddAsync(TravelPurpose travelPurpose);
        Task<TravelPurpose> UpdateAsync(TravelPurpose travelPurpose);
        Task DeleteAsync(int id);
        Task<TravelPurpose> GetAsyncById(int id);
        Task<TravelPurpose> GetAsync(Expression<Func<TravelPurpose, bool>> predicate);
        Task<IEnumerable<TravelPurpose>> GetAllAsync();
        IQueryable<TravelPurpose> GetAll();
        IEnumerable<TravelPurpose> GetAll(Expression<Func<TravelPurpose, bool>> predicate, int start = 0, int count = 25);
        Task<IEnumerable<TravelPurpose>> GetAllAsync(Expression<Func<TravelPurpose, bool>> predicate, int start = 0, int count = 25);
    }
}
