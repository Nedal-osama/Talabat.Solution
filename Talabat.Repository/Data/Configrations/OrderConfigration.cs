using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;
using Order = Talabat.Core.Order_Aggregrate.Order;

namespace Talabat.Repository.Data.Configrations
{
	public class OrderConfigration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.Status).HasConversion(
				outputStatus => outputStatus.ToString(),
				outputStatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), outputStatus));
			builder.OwnsOne(o => o.ShippingAddress, shippingAddress => shippingAddress.WithOwner());
			builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
			builder.HasOne(o=>o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
		}
	}
}
