using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCacheResponseCaching.Models;

namespace NCacheResponseCaching
{
    public class HotelService: IHotelService
    {
        private readonly HotelDbContext _context;
        public HotelService(HotelDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<List<Hotel>> GetHotels()
        
        {
            //CachingOptions options = new CachingOptions
            //{
            //    StoreAs = StoreAs.SeperateEntities
            //};
            //var items = (from r in _context.Hotels select r).FromCache(options).ToList();
         
            return await _context.Hotels.ToListAsync();
        }

    }
}
