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
        public void AddItemToCart(){
            using (var context = new P3Referential(
                serviceProvider.GetRequiredService<Microsoft.EntityFrameworkCore.DbContextOptions<P3Referential>>()))
            {
                Cart cart = new Cart();

                context.Product product1 = new context.Product{

                }

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            Assert.NotEmpty(cart.Lines);
            Assert.Single(cart.Lines);
            Assert.Equal(2, cart.Lines.First().Quantity);
            }
        }

        /// <summary>
        /// [Fact]
        /// </summary>
        /// public void GetAverageValue() {
            /// Cart cart = new Cart();

        /// }
    }
}