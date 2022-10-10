using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntrepidProducts.WebApiTest.Models
{
    public static class ModelValidator
    {
        public static List<ValidationResult> Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}