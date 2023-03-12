using Microsoft.AspNetCore.Mvc;
using NCacheResponseCaching.Models;

namespace NCacheResponseCaching
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetHotels();
    }
}
