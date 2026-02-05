using RentARide.Domain.Common;
using RentARide.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RentARide.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Pending;

        
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }

        
        public User? User { get; set; }
        public Vehicle? Vehicle { get; set; }
        public ICollection<RentalAmenity> RentalAmenities { get; set; } = new List<RentalAmenity>();
    }
}
