using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class QuantityRange : ValidationAttribute {
        private readonly int _minRange;

        public QuantityRange(int minRange)
        {
            _minRange = minRange;
        }

        public override bool IsValid(object value)
        {

            var quantityValue = value as string;

            if (quantityValue == null) {
                return true;
            }

            if (!int.TryParse(quantityValue, out int intValue)) {
                return true; // typeChecker will catch if it fails here instead
            }

            if (intValue < _minRange)
            {
                return false;
            }
            return true;
        }

    }
}