using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests {
    public class ProductControllerTests {

        [Fact]
        public void ProductControllerDeleteProductTest() {

            // Arrange: mocks for product repo
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockCart = new Mock<Cart>().Object;
            var languageService = new Mock<LanguageService>().Object;

            // Arrange more: Mock DbContext
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


            // Arrange: real components
            ProductRepository productRepository = new ProductRepository(mockContext.Object);
            ProductService productService = new ProductService(mockCart, productRepository, mockOrderRepository.Object, localizer);
            ProductController productController = new ProductController(productService, languageService);

            // Act
            productController.DeleteProduct(1);
            var viewResult = productController.Admin() as ViewResult;
            var modelResult = viewResult.Model as IEnumerable<ProductViewModel>;
            
            // Assert
            Assert.NotNull(viewResult.Model);
            Assert.Single(modelResult);
            Assert.Equal(2, modelResult.FirstOrDefault().Id);
        }
    
    }
}