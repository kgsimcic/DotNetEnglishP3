using Xunit;
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
            Assert.IsSame("NOKIA OEM BL-5J", product.Name);
            Assert.IsEqual(895.00, product.Price);
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

            productService.UpdateProductQuantities(cart);

            Assert.IsEqual(8, products.where(p => p.id == 1).First().Stock);
            Assert.IsEqual(49, products.where(p => p.id == 5).First().Stock);
            Assert.IsEqual(37, products.where(p => p.id == 4).First().Stock);

            ///test that static cart retains the updates.
    
            Cart cart = new Cart();
            productRepository = new ProductRepository();
            orderRepository = new OrderRepository();
            productService = new ProductService(productRepository, orderRepository);
            
            products = productService.GetAllProducts();
            cart.AddItem(products.where(p => p.id == 1).First(), 1);
            cart.AddItem(products.where(p => p.id == 5).First(), 2);
            cart.AddItem(products.where(p => p.id == 4).First(), 1);

            productService.UpdateProductQuantities(cart);

            Assert.IsEqual(7, products.where(p => p.id == 1).First().Stock);
            Assert.IsEqual(47, products.where(p => p.id == 5).First().Stock);
            Assert.IsEqual(36, products.where(p => p.id == 4).First().Stock);
        }
    }
}