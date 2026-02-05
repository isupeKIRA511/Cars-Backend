namespace RentARide.Application.DTOs.Rentals
{
    public class CreateRentalRequest
    {
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> AmenityIds { get; set; } = new List<Guid>();
        public string? PromoCode { get; set; }
    }
}
