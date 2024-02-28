using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]

        public int Id { get; set; }

        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "MissingQuantity")]
        [QuantityTypeInt(ErrorMessage = "QuantityNotAnInteger")]
        [QuantityRange(0, ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [PriceTypeNum(ErrorMessage = "PriceNotANumber")]
        [PriceRange(0, ErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
