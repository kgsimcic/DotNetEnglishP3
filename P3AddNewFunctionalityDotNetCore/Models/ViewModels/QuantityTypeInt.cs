using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class QuantityTypeInt : ValidationAttribute {
        public override bool IsValid(object value)
        {
            string stringValue = value as string;

            if (stringValue == null)
                return true;

            if (int.TryParse(stringValue, out int result))
                return true;

            return false;
        }
    }
}