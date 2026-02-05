using System;
using System.Threading.Tasks;

namespace RentARide.Application.Common.Interfaces
{
    public interface IHolidayService
    {
        Task<bool> IsHolidayAsync(DateTime date, string countryCode = "DE");
    }
}
