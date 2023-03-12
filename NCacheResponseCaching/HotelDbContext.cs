using Microsoft.EntityFrameworkCore;
using NCacheResponseCaching.Models;
using System.Collections.Generic;

namespace NCacheResponseCaching
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
