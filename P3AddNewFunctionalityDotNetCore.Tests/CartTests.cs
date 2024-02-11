using Xunit;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    public class CartTests {

        [Fact]
        public void TestAddItem() {

            // Act
            Cart cart = new Cart();
            var result = TestDataHelper.GetFakeProducts();

            // Arrange
            cart.AddItem(result.FirstOrDefault(), 2);
            cart.AddItem(result.FirstOrDefault(), 1);

            // Assert
            Assert.NotEmpty(cart.Lines);
            Assert.Single(cart.Lines);
            Assert.Equal(3, cart.Lines.First().Quantity);
        }

        [Fact]
        public void TestRemoveLine() {

            // Act
            Cart cart = new Cart();
            var result = TestDataHelper.GetFakeProducts();

            // Arrange
            cart.AddItem(result.FirstOrDefault(), 2);
            cart.AddItem(result.ElementAt(1), 1);
            cart.RemoveLine(result.FirstOrDefault());

            // Assert
            Assert.NotEmpty(cart.Lines);
            Assert.Single(cart.Lines);
            Assert.Equal(2, cart.Lines.FirstOrDefault().Product.Id);
        }

        [Fact]
        public void TestGetAverageValue() {
            // Act
            Cart cart = new Cart();
            var result = TestDataHelper.GetFakeProducts();

            // Arrange
            cart.AddItem(result.FirstOrDefault(), 1);
            cart.AddItem(result.ElementAt(1), 1);
            var averageValue = cart.GetAverageValue();

            // Assert
            // Answer should be (15 + 45)/2 = 30
            Assert.Equal(30, averageValue);
        }

        [Fact]
        public void TestGetTotalValue() {
            // Act
            Cart cart = new Cart();
            var result = TestDataHelper.GetFakeProducts();

            // Arrange
            cart.AddItem(result.FirstOrDefault(), 1);
            cart.AddItem(result.ElementAt(1), 1);
            var totalValue = cart.GetTotalValue();

            // Assert
            // Answer should be (15 + 45) = 60
            Assert.Equal(60, totalValue);
        }

        // Integation Test
        [Fact]
        public void TestClear() {
            // Act
            Cart cart = new Cart();
            var result = TestDataHelper.GetFakeProducts();

            // Arrange
            cart.AddItem(result.FirstOrDefault(), 2);
            cart.AddItem(result.ElementAt(1), 1);

            // Assert
            Assert.NotEmpty(cart.Lines);

            // Arrange again
            cart.Clear();

            // Assert final to test functionality of Clear and empty case for average/total calculations
            Assert.Empty(cart.Lines);
            Assert.Equal(0, cart.GetAverageValue());
            Assert.Equal(0, cart.GetTotalValue());
        }
    }
}