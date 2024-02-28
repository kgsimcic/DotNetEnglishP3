using System.ComponentModel.DataAnnotations;
using System;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class PriceRange : ValidationAttribute
    {
        private readonly double _minRange;
        public PriceRange(double minRange)
        {
            _minRange = minRange;
        }

        public override bool IsValid(object value)
        {

            var priceValue = value as string;

            if (priceValue == null) {
                return true;
            }

            if (!double.TryParse(priceValue, out double doubleValue)) {
                return true;
            }

            if (doubleValue < _minRange)
            {
                return false;
            }
            return true;
        }
    }
}