using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(12,2)");

            builder
                .Property(p => p.Category)
                .HasConversion(
                    x => x.ToString(),
                    x => (Category) Enum.Parse(typeof(Category), x));
        }
    }
}