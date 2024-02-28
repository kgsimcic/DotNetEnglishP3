using System.ComponentModel.DataAnnotations;
using System;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class PriceTypeNum : ValidationAttribute {

        public override bool IsValid(object value)
        {
            string stringValue = value as string;

            if (stringValue == null)
                return true;

            if (double.TryParse(stringValue, out double result))
                return true; // Successfully parsed as a double

            return false; // Not a valid double
        }
    }
}