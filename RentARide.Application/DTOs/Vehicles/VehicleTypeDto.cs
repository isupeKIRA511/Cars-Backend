namespace RentARide.Application.DTOs.Vehicles
{
    public class VehicleTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
