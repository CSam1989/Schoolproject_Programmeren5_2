using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.FamilyName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Street)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.HouseNr)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.HouseBus)
                .HasMaxLength(4);

            builder.Property(c => c.PostalCode)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(c => c.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.StreetBilling)
                .HasMaxLength(100);

            builder.Property(c => c.HouseNrBilling)
                .HasMaxLength(5);

            builder.Property(c => c.HouseBusBilling)
                .HasMaxLength(4);

            builder.Property(c => c.PostalCodeBilling)
                .HasMaxLength(6);

            builder.Property(c => c.CityBilling)
                .HasMaxLength(100);
        }
    }
}