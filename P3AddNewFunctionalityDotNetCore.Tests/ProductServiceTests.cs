using Xunit;
using System;
using Moq;
using System.Linq;
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
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
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
        public void TestGetAllProducts()
        {
            // Arrange
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockCart = new Mock<Cart>().Object;

            // set up mock product repository
            var mockProductRepository = new Mock<IProductRepository>();
            List<Product> fakeProducts = TestDataHelper.GetFakeProducts();
            IEnumerable<Product> fakeProductEntities = fakeProducts.Where(p => p.Id > 0);
            mockProductRepository.Setup(x => x.GetAllProducts()).Returns(fakeProductEntities);

            // finally the service itself
            IProductService productService = new ProductService(mockCart, mockProductRepository.Object, mockOrderRepository.Object, localizer);
            
            // Act
            var products = productService.GetAllProducts();

            // Assert
            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count);           
        }

        [Fact]
        public void TestGetProductById()
        {
            // Arrange
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockCart = new Mock<Cart>().Object;

            // set up mock product repository
            var mockProductRepository = new Mock<IProductRepository>();
            List<Product> fakeProducts = TestDataHelper.GetFakeProducts();
            IEnumerable<Product> fakeProductEntities = fakeProducts.Where(p => p.Id > 0);
            mockProductRepository.Setup(x => x.GetAllProducts()).Returns(fakeProductEntities);

            // finally the service itself
            IProductService productService = new ProductService(mockCart, mockProductRepository.Object, mockOrderRepository.Object, localizer);
            
            // Act
            var product = productService.GetProductById(2);

            // Assert
            Assert.Equal(2, product.Id);
            Assert.Same("more words", product.Description);
            Assert.Same("product2", product.Name);  
        }

        [Fact]
        public void TestUpdateProductQuantities()
        {
            // Arrange
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var mockOrderRepository = new Mock<IOrderRepository>();
            var cart = new Cart();
            // IEnumerable<CartLine> fakeCartLines = TestDataHelper.GetFakeCartLines();
            // mockCart.Setup(x => x.Lines).Returns(fakeCartLines);

            // set up mock product repository
            var mockProductRepository = new Mock<IProductRepository>();
            List<Product> fakeProducts = TestDataHelper.GetFakeProducts();
            IEnumerable<Product> fakeProductEntities = fakeProducts.Where(p => p.Id > 0);
            mockProductRepository.Setup(x => x.GetAllProducts()).Returns(fakeProductEntities);
            // mockProductRepository.Setup(x => x.UpdateProductStocks(2, 1)).Returns(fakeProducts.FirstOrDefault())

            // set up non empty cart
            cart.AddItem(fakeProducts.FirstOrDefault(), 1);

            // finally the service itself
            IProductService productService = new ProductService(cart, mockProductRepository.Object, mockOrderRepository.Object, localizer);

            // Act
            var productsBefore = productService.GetAllProducts();

            // Assert
            Assert.NotEmpty(productsBefore);
            Assert.Equal(2, productsBefore.Count);  

            // Act again
            productService.UpdateProductQuantities();
            var productsAfter = productService.GetAllProducts();

            // Assert final
            Assert.NotEmpty(productsAfter);
            Assert.Single(productsAfter);
            Assert.Equal(2, productsAfter.FirstOrDefault().Id);

        }
    }
}