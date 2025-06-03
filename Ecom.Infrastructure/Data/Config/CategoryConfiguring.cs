using Ecom.Core.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class CategoryConfiguring : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Id).IsRequired();

            //builder.HasMany(X => X.Products)
            //       .WithOne(x => x.Category)
            //       .HasForeignKey(x => x.CategoryId);

            builder.HasData(new Category
            {
                Id = 1,
                Name = "test",
                Description = "test"


            });

        }
    }
}
