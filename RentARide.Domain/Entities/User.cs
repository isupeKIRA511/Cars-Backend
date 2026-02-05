using RentARide.Domain.Common;
using RentARide.Domain.Enums;
using System.Collections.Generic;

namespace RentARide.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Customer;

        
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
