using Xunit;
using Moq;
using System;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    // Unit tests for ProductViewModel
    public class ProductViewModelTests {

        [Fact]
        public void TestDataAnnotations1() {

            // Arrange 
            var product = new ProductViewModel {
                Name = null,
                Description = null,
                Details = null,
                Stock = null,
                Price = null,
            };

            // Act
            var validationErrors = TestValidator.Validate(product);

            // Assert
            Assert.Equal(3, validationErrors.Count);
            Assert.True(validationErrors.Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("MissingName")));

        }

        [Fact]
        public void TestDataAnnotations2() {

            // Arrange 
            var product = new ProductViewModel {
                Name = "test",
                Description = null,
                Details = null,
                Stock = "test",
                Price = "1.0",
            };

            // Act
            var validationErrors = TestValidator.Validate(product);

            // Assert
            Assert.Equal(1, validationErrors.Count());
            Assert.True(validationErrors.FirstOrDefault().ErrorMessage.Contains("QuantityNotAnInteger"));

        }

        [Fact]
        public void TestDataAnnotations3() {

            // Arrange 
            var product = new ProductViewModel {
                Name = "test",
                Description = null,
                Details = null,
                Stock = null,
                Price = "test",
            };

            // Act
            var validationErrors = TestValidator.Validate(product);

            // Assert
            Assert.Equal(2, validationErrors.Count());
            Assert.True(validationErrors.Any(v => v.ErrorMessage.Contains("PriceNotANumber")));
            Assert.True(validationErrors.Any(v => v.ErrorMessage.Contains("MissingQuantity")));
        }

        [Fact]
        public void TestDataAnnotations4() {

            // Arrange 
            var product = new ProductViewModel {
                Name = "test",
                Description = null,
                Details = null,
                Stock = "1",
                Price = "1.0",
            };

            // Act
            var validationErrors = TestValidator.Validate(product);

            // Assert
            Assert.Equal(0, validationErrors.Count());

        }
    }
}