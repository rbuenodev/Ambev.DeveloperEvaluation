using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasQueryFilter(ci => !ci.IsDeleted);
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(ci => ci.Quantity).IsRequired().HasColumnType("int");
            builder.Property(ci => ci.Price).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(CartItem => CartItem.IsDeleted).IsRequired().HasColumnType("bool").HasDefaultValue(false);

            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
