using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(cart => cart.Id);

            builder.Property(cart => cart.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(cart => cart.CreatedAt).IsRequired();
            builder.Property(cart => cart.UpdatedAt);
            builder.Property(cart => cart.Status).IsRequired();
            builder.Property(cart => cart.Branch).IsRequired().HasConversion<string>();
            builder.Property(cart => cart.SaleNumber).HasMaxLength(255);

            builder.HasMany(cart => cart.Items)
                .WithOne()
                .HasForeignKey(cartItem => cartItem.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cart => cart.User)
                .WithMany()
                .HasForeignKey(cart => cart.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
