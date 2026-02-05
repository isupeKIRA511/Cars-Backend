using Microsoft.EntityFrameworkCore;
using RentARide.Domain.Entities;
using System.Reflection;

using RentARide.Application.Common.Interfaces;

namespace RentARide.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RentalAmenity> RentalAmenities { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Vehicle>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Rental>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PromoCode>().HasQueryFilter(e => !e.IsDeleted);
            
        }
    }
}
