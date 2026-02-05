using RentARide.Domain.Common;
using System;

namespace RentARide.Domain.Entities
{
    public class RentalAmenity : BaseEntity
    {
        public Guid RentalId { get; set; }
        public Guid AmenityId { get; set; }

        public Rental? Rental { get; set; }
        public Amenity? Amenity { get; set; }
    }
}
