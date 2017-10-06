﻿using AspNetCoreWorkshop.Controllers;
using AspNetCoreWorkshop.Data;
using AspNetCoreWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq;

namespace WorkshopTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void GetReturnsListOfProducts()
        {

            var logger = Substitute.For<ILogger<ProductsController>>();
            using (var context = CreateTestStoreContext())
            {
                // arrange
                var productsContoller = new ProductsController(context, logger);

                // act
                var result = productsContoller.Get().Result;

                // assert
                CollectionAssert.AreEqual(result.ToList(), context.Products.ToList());
            }
        }

        [TestMethod]
        public void GetByIdWithInvalidIdReturnsNotFoundResult()
        {
            using (var context = CreateTestStoreContext())
            {
                var logger = Substitute.For<ILogger<ProductsController>>();

                // arrange
                var productsContoller = new ProductsController(context, logger);

                // act
                var result = productsContoller.Get(100).Result;

                // assert
                Assert.AreEqual(typeof(NotFoundResult), result.GetType());
            }
        }

        private StoreContext CreateTestStoreContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestProductsControllerDatabase")
                .Options;

            var context = new StoreContext(options);

            var products = new Product[]
            {
                new Product{ Name = "Widget 1.0" },
                new Product{ Name = "Super Widget" },
                new Product{ Name = "Widget Mini" },
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            return context;
        }
    }
}
