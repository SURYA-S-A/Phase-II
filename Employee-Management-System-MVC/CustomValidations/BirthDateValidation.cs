using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_MVC.CustomValidations
{
    public class BirthDateValidation :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? birthdate = value as DateTime?;

            if (birthdate.HasValue && birthdate.Value <= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
