using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentARide.Infrastructure.Persistence;

namespace RentARide.Infrastructure.BackgroundJobs
{
    public class OverdueRentalJob
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OverdueRentalJob> _logger;

        public OverdueRentalJob(ApplicationDbContext context, ILogger<OverdueRentalJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CheckOverdueRentals()
        {
            var now = DateTime.UtcNow;

            var overdueRentals = await _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Vehicle)
                .Where(r => r.EndDate < now && !r.IsDeleted) 
                .ToListAsync();

            if (overdueRentals.Any())
            {
                foreach (var rental in overdueRentals)
                {
                   _logger.LogWarning("OVERDUE RENTAL WARNING: Rental {RentalId} for User {UserEmail} (Vehicle {LicensePlate}) is overdue since {EndDate}.", 
                       rental.Id, rental.User?.Email, rental.Vehicle?.LicensePlate, rental.EndDate);
                }
            }
        }
    }
}
