using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        public ValidateObjectAttribute()
        {
        }

        public override bool RequiresValidationContext => true;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(value, new ValidationContext(value), validationResults, validateAllProperties: true))
                return ValidationResult.Success;

            var message = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
            var mn = validationResults.SelectMany(vr => vr.MemberNames);

            return new ValidationResult(message, mn);
        }
    }
}