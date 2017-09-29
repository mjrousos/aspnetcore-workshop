﻿using Lab6.Models;
using Foo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }

        /// <summary>
        /// This method can be used to customize ORM beahviors
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For example, table names for .NET classes can be customized
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<OrderLineItem>().ToTable("OrderLineItem");

            // Or, relationships between entities could be explicitly defined
            // or keys could be defined, etc.
        }
    }
}
