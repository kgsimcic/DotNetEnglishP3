using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Tests {

    public static class TestValidator {
        
        public static List<ValidationResult> Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}