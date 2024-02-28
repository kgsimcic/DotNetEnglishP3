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
            var productData = TestDataHelper.GetFakeProducts().AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productData.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productData.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productData.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productData.GetEnumerator());

            // set up behavior of mock dbcontext
            var mockContext = new Mock<P3Referential>();
            mockContext.Setup(x => x.Product).Returns(mockSet.Object);

            // Arrange: real components
            ProductRepository productRepository = new ProductRepository(mockContext.Object);
            IProductService productService = new ProductService(mockCart, productRepository, mockOrderRepository.Object, localizer);
            ProductController productController = new ProductController(productService, languageService);

            // Act
            var deleteResult = productController.DeleteProduct(1) as ViewResult;
            // var viewResult = productController.Index() as ViewResult;

            // Assert
            Assert.NotNull(deleteResult.Model);
            Assert.IsType<List<ProductViewModel>>(deleteResult.Model);
            Assert.Single(deleteResult.Model as IEnumerable<ProductViewModel>);


        }
    }
}