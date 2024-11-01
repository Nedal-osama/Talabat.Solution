using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Repository.Data.Configrations
{
	public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(orderItem => orderItem.Product, product =>
			{
				product.WithOwner();  // This line ensures that the owned entity is linked to the owning entity
				product.Property(p => p.ProductName).IsRequired();
				// You can add other property configurations for ProductItemOrdered here if needed
			});

			// Configure the Price property
			builder.Property(item => item.Price).HasColumnType("decimal(18,2)");
		}
	}
}
