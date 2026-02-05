using RentARide.Domain.Common;
using System;

namespace RentARide.Domain.Entities
{
    public class VehicleMaintenance : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime MaintenanceDate { get; set; }

        
        public Guid VehicleId { get; set; }

        
        public Vehicle? Vehicle { get; set; }
    }
}
