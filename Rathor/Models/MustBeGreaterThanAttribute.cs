using System;
using System.ComponentModel.DataAnnotations;

namespace Rathor.Models
{
    public class MustBeGreaterThanAttribute: ValidationAttribute
    {
       private readonly string _otherProperty;

       public MustBeGreaterThanAttribute(string otherProperty, string errorMessage) : base(errorMessage)
       {
        _otherProperty = otherProperty;
       }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(_otherProperty);
        var otherValue = otherProperty.GetValue(validationContext.ObjectInstance, null);
        var thisDateValue = Convert.ToDateTime(value);
        var otherDateValue = Convert.ToDateTime(otherValue);

        if (thisDateValue > otherDateValue)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
    }
}