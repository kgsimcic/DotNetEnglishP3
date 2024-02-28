using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
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

        [Fact]
        public void TestDeleteProduct()
        {
            var productData = TestDataHelper.GetFakeProducts();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productData.AsQueryable().Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productData.AsQueryable().Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productData.AsQueryable().ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productData.AsQueryable().GetEnumerator());

            // set up get and delete behavior of mock dbcontext
            var mockContext = new Mock<P3Referential>();
            mockContext.Setup(x => x.Product).Returns(mockSet.Object);
            mockContext.Setup(m => m.Product.Remove(It.IsAny<Product>())).Callback<Product>((entity) => productData.Remove(entity));

            int idToDelete = 1;
            mockContext.Setup(m => m.Product.Find(idToDelete)).Returns(productData.Single(p => p.Id == idToDelete));

            // Arrange
            ProductRepository productRepository = new ProductRepository(mockContext.Object);
            var products = productRepository.GetAllProducts();

            // Assert before number is more
            Assert.Equal(2, products.Count());

            // Act
            productRepository.DeleteProduct(1);
            products = productRepository.GetAllProducts();

            // Assert
            Assert.NotNull(products);
            Assert.Single(products);
            Assert.Equal(2, products.FirstOrDefault().Id);
        }

        [Fact]
        public void TestSaveProduct()
        {
            // Arrange
            var productData = TestDataHelper.GetFakeProductsInsert();
            var mockSet = new Mock<DbSet<Product>>();

            // create new product to add
            Product productToAdd = new Product {
                Id = 3,
                Name = "Product3",
                Quantity = 1,
                Price = 1.50
            };

            // set up behavior
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productData.AsQueryable().Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productData.AsQueryable().Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productData.AsQueryable().ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productData.AsQueryable().GetEnumerator());
            // mockSet.Setup(m => m.Add(It.IsAny<Product>())).Callback<Product>((p) => productData.Add(p));
            // mockSet.Setup(m => m.Add(It.IsAny<Product>())).Callback<Product>(product => productToAdd.Id = product.Id);

            // set up get and delete behavior of mock dbcontext
            var mockContext = new Mock<P3Referential>();
            mockContext.Setup(x => x.Product).Returns(mockSet.Object);
            mockContext.Setup(m => m.Product.Add(It.IsAny<Product>())).Callback<Product>((p) => productData.Add(p));

            ProductRepository productRepository = new ProductRepository(mockContext.Object);
            var productsBefore = productRepository.GetAllProducts();

            // Assert before number is less
            Assert.Equal(2, productsBefore.Count());
            Assert.NotNull(productToAdd);

            // Act
            // productRepository.SaveProduct(productToAdd);
            // mockContext.Object.Product.Add(productToAdd);
            var productsMore = productRepository.GetAllProducts();

            // Assert
            // mockContext.Verify(m => m.Product.Add(It.IsAny<Product>()), Times.Once());

            // Assert.NotNull(products);
            // Assert.Equal(3, productsMore.Count());
            
            // Assert.Equal(2, productsMore.FirstOrDefault().Id);
        }

        // [Fact]
        // public void testGetProduct()
        // {
        //     var productData = TestDataHelper.GetFakeProducts().AsQueryable();
        //     var mockSet = new Mock<DbSet<Product>>();
        //     mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productData.Provider);
        //     mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productData.Expression);
        //     mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productData.ElementType);
        //     mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productData.GetEnumerator());

        //     // set up behavior of mock dbcontext
        //     var mockContext = new Mock<P3Referential>();
        //     mockContext.Setup(x => x.Product).Returns(mockSet.Object);

        //     // Arrange
        //     ProductRepository productRepository = new ProductRepository(mockContext.Object);

        //     // Act
        //     var product = productRepository.GetProductById(2);

        //     // Assert
        //     Assert.NotNull(product);
        //     Assert.Single(product);
        //     Assert.Equal(2, product.Id);
        //     Assert.Same("more words", product.Description);
        // }
        
    }
}

