using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    public class ProductRepositoryTests {

        [Fact]
        public void TestGetAllProducts()
        {
            var productData = TestDataHelper.GetFakeProducts().AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productData.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productData.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productData.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productData.GetEnumerator());

            // set up behavior of mock dbcontext
            var mockContext = new Mock<P3Referential>();
            mockContext.Setup(x => x.Product).Returns(mockSet.Object);

            // Arrange
            ProductRepository productRepository = new ProductRepository(mockContext.Object);

            // Act
            var products = productRepository.GetAllProducts();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(2, products.Count());

        }
        
    }
}

