using RentARide.Domain.Common;
using System.Collections.Generic;

namespace RentARide.Domain.Entities
{
    public class VehicleType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }

        
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
