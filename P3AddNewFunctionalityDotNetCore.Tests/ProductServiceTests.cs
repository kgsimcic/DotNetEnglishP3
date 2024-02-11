using Xunit;
using System;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyModel.Resolution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Tests;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Product {get; set;}
    }
    
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        /// 

        [Fact]
        public void TestGetProductById()
        {
            // Arrange
            // var mockSet = new Mock<DbSet<Product>>();
            // var mockContext = new Mock<ProductContext>();
            // mockContext.Setup<DbSet<Product>>(x => x.Product).ReturnsDbSet(TestDataHelper.GetFakeProducts());

            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var mockOrderRepository = new Mock<OrderRepository>().Object;
            var mockProductRepository = new Mock<ProductRepository>();
            mockProductRepository.Setup(x => x.GetAllProducts()).Returns(TestDataHelper.GetFakeProducts());
            var mockCart = new Mock<Cart>().Object;
            IProductService productService = new ProductService(mockCart, mockProductRepository.Object, mockOrderRepository, localizer);
            var product = productService.GetAllProducts();

            // Act

            // Assert
            // Assert.Equals(1, product.Id);
            // Assert.Same("words", product.Description);
            // Assert.Same("product1", product.Name);            
        }
    }
}