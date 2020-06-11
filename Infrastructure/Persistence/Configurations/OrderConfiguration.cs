using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>

    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(o => o.StreetShipping)
                .HasMaxLength(100);

            builder
                .Property(o => o.HouseNrShipping)
                .HasMaxLength(5);

            builder
                .Property(o => o.HouseBusShipping)
                .HasMaxLength(3);

            builder
                .Property(o => o.PostalcodeShipping)
                .HasMaxLength(6);

            builder.Property(o => o.CityShipping)
                .HasMaxLength(100);
        }
    }
}