using System;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System.Threading.Tasks;
using P3AddNewFunctionalityDotNetCore.Models;

namespace P3AddNewFunctionalityDotNetCore.Tests {
    // public class CartLine
    // {
    //     public int OrderLineId { get; set; }
    //     public Product Product { get; set; }
    //     public int Quantity { get; set; }
    // }

    public class TestDataHelper {
        // method for mock to return fake products
        public static List<Product> GetFakeProducts()
        {
            return new List<Product>()
            {
                new Product {
                    Id = 1,
                    Description = "words",
                    Name = "product1",
                    Quantity = 1,
                    Price = 15.00
                },
                new Product {
                    Id = 2,
                    Description = "more words",
                    Name = "product2",
                    Quantity = 1,
                    Price = 45
                }
            };
        }

        public static List<CartLine> GetFakeCartLines()
        {
            return new List<CartLine>()
            {
                new CartLine {
                    OrderLineId = 1,
                    Product = new Product {
                        Id = 2,
                        Description = "more words",
                        Name = "product2",
                        Quantity = 1,
                        Price = 45
                    },
                    Quantity = 1
                }
            };
        }
        public static List<Order> GetFakeOrders()
        {
            return new List<Order>()
            {
                new Order {
                    Id = 1,
                    Address = "Sesame Street",
                    City = "PBS City",
                    Country = "USA",
                    Date = new DateTime(2008, 6, 1),
                    Name = "Burt's order",
                    Zip = "00000",
                    
                }
            };
        }
    }
}