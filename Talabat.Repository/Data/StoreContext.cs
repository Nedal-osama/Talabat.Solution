﻿using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Repository.Data.Configrations;
using Order = Talabat.Core.Order_Aggregrate.Order;
using Product = Talabat.Core.Order_Aggregrate.ProductItemOrdered;

namespace Talabat.Repository.Data
{
	public class StoreContext:DbContext
	{
		public object products;
		internal object deliveryMethod;

		public StoreContext(DbContextOptions<StoreContext>options):base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.Entity<OrderItem>().OwnsOne(o => o.Product);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
		

	}
}
