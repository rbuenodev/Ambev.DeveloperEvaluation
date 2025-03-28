using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Title).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(p => p.Description).HasMaxLength(256);
            builder.Property(p => p.Category).HasMaxLength(50);
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.UpdatedDate);
            builder.Property(p => p.Status);
            builder.Property(propertyExpression: p => p.IsDeleted).IsRequired().HasColumnType("bool").HasDefaultValue(false);
        }
    }
}
