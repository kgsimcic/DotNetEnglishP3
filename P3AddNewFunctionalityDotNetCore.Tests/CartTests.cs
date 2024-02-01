using XUnit;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    public class CartTests {

        [Fact]
        public void AddItemToCart(){
            Cart cart = new Cart();
            Product product1 = new Product(1, 0, 20, "name", "description");
            Product product2 = new Product(1, 0, 20, "name", "description");

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            Assert.NotEmpty(cart.Lines);
            Assert.Single(cart.Lines);
            Assert.Equal(2, cart.Lines.First().Quantity);
        }

        [Fact]
        public void GetAverageValue() {
            Cart cart = new Cart();
            
        }
    }
}