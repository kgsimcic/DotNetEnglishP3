using XUnit;
using P3AddNewFunctionalityDotNetCore.Models;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    public class ProductTests {

        /// Tests creation of product and whether correct errors are created for different issues.
        /// 

        [Fact] 
        public void CreateProduct1() {

            // all arguments missing

            Product  product1 = new Product("", null, "", "", null);
        }

        [Fact]
        public void InvalidQuantities() {

            // first should have negative quantity, second should have non-integer quantity

            Product product1 = new Product("")
        }
    }
}