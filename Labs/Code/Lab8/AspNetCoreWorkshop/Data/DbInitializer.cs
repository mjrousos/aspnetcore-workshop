using AspNetCoreWorkshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            // Creates the database if it doesn't exist
            context.Database.EnsureCreated();

            // Checks to see if any products exist
            if (context.Products.Any())
            {
                return; // DB already has been seeded
            }

            // Initial products
            var products = new Product[]
            {
                new Product{ Name = "Widget 1.0" },
                new Product{ Name = "Super Widget" },
                new Product{ Name = "Widget Mini" },
                new Product{ Name = "Widget Xtreme" },
                new Product{ Name = "Jumbo Widget" },
                new Product{ Name = "Widget 2.0" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // Initial orders
            var orders = new Order[]
            {
                new Order { Description = "First order", DateCreated = DateTime.Now.AddDays(-10), Completed = true },
                new Order { Description = "Second order", DateCreated = DateTime.Now }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();

            // Initial order line items
            var orderItems = new OrderLineItem[]
            {
                new OrderLineItem{ Order = orders[0], Product = products[0], Quantity = 1 },
                new OrderLineItem{ Order = orders[0], Product = products[3], Quantity = 2 },

                new OrderLineItem{ Order = orders[0], Product = products[1], Quantity = 2 },
                new OrderLineItem{ Order = orders[1], Product = products[3], Quantity = 1 },
                new OrderLineItem{ Order = orders[1], Product = products[5], Quantity = 1 }
            };

            context.OrderLineItems.AddRange(orderItems);
            context.SaveChanges();
        }
    }
}
