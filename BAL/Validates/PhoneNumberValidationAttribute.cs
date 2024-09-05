using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validates
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Phone number is required.");
            }

            // Additional custom validation logic here (e.g., regex pattern)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^[0-9]{10}$"))
            {
                return new ValidationResult("Invalid phone number format.");
            }

            return ValidationResult.Success;
        }
    }

}
