using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentARide.Domain.Entities;

namespace RentARide.Infrastructure.Persistence.Configurations
{
    public class RentalAmenityConfiguration : IEntityTypeConfiguration<RentalAmenity>
    {
        public void Configure(EntityTypeBuilder<RentalAmenity> builder)
        {
            builder.HasKey(ra => new { ra.RentalId, ra.AmenityId });

            builder.HasOne(ra => ra.Rental)
                .WithMany(r => r.RentalAmenities)
                .HasForeignKey(ra => ra.RentalId);

            builder.HasOne(ra => ra.Amenity)
                .WithMany(a => a.RentalAmenities)
                .HasForeignKey(ra => ra.AmenityId);
        }
    }
}
