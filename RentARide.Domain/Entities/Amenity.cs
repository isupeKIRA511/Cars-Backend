using RentARide.Domain.Common;
using System.Collections.Generic;

namespace RentARide.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        
        public ICollection<RentalAmenity> RentalAmenities { get; set; } = new List<RentalAmenity>();
    }
}
