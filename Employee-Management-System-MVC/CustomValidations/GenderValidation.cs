using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Employee_Management_System_MVC.CustomValidations
{
    public class GenderValidation :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "M" || value.ToString() == "F" || value.ToString() == "O")
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Select gender");
            }
        }
    }
}
