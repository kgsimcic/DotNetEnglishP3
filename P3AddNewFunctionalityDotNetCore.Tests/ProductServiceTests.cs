using Xunit;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyModel.Resolution;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void ProductTest()
        {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            var products = productService.GetAllProducts();

            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void GetProductFromId() {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            int id = 5;

            Product product = productService.GetProductById(id);
            Assert.Same("NOKIA OEM BL-5J", product.Name);
            Assert.Equal(895.00, product.Price);
        }

        /// Integration Test
        [Fact]
        public void UpdateProductQuantities(){
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            var products = productService.GetAllProducts();
            cart.AddItem(products.where(p => p.id == 1).First(), 2);
            cart.AddItem(products.where(p => p.id == 5).First(), 1);
            cart.AddItem(products.where(p => p.id == 4).First(), 3);

            productService.UpdateProductQuantities();

            Assert.Equal(8, products.Where(p => p.id == 1).First().Stock);
            Assert.Equal(49, products.Where(p => p.id == 5).First().Stock);
            Assert.Equal(37, products.Where(p => p.id == 4).First().Stock);

            ///test that static cart retains the updates.
    
            cart = new Cart();
            productRepository = new ProductRepository();
            orderRepository = new OrderRepository();
            productService = new ProductService(productRepository, orderRepository);
            
            products = productService.GetAllProducts();
            cart.AddItem(products.Where(p => p.id == 1).First(), 1);
            cart.AddItem(products.Where(p => p.id == 5).First(), 2);
            cart.AddItem(products.Where(p => p.id == 4).First(), 1);

            productService.UpdateProductQuantities(cart);

            Assert.Equal(7, products.Where(p => p.id == 1).First().Stock);
            Assert.Equal(47, products.Where(p => p.id == 5).First().Stock);
            Assert.Equal(36, products.Where(p => p.id == 4).First().Stock);
        }
    }
}