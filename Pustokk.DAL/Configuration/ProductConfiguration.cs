using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
           .Property(p => p.Name)
           .IsRequired()
           .HasMaxLength(50);


            builder.Property(p => p.Price)
             .IsRequired()
             .HasColumnType("decimal(10,2)")
             .HasDefaultValue(0);
        }
    }
}
