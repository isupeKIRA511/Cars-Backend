using RentARide.Domain.Common;
using RentARide.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RentARide.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Available;

        
        public Guid VehicleTypeId { get; set; }

        
        public VehicleType? VehicleType { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<VehicleMaintenance> Maintenances { get; set; } = new List<VehicleMaintenance>();
    }
}
