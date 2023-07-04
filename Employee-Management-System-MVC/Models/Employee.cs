using Employee_Management_System_MVC.CustomValidations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_MVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabets and spaces are allowed.")]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Name")]        
        public string EmployeeName { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        [DisplayName("Gender")]
        [GenderValidation]
        public char EmployeeGender { get; set; }


        [Required(ErrorMessage = "Birth date is required")]
        [DisplayName("Birth date")]
        [BirthDateValidation(ErrorMessage = "Not valid")]
        public DateTime EmployeeBirthDate { get; set; }

        
        [Required(ErrorMessage = "Position is required")]
        [DisplayName("Position")]
        public int FK_PositionId { get; set; }


        [Required(ErrorMessage = "Salary is required")]
        [Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 10000000")]
        [DisplayName("Salary")]
        public decimal? EmployeeSalary { get; set; }


        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Mobile number should contain 10 digits.")]
        [DisplayName("Mobile Number")]
        public string EmployeeMobileNumber { get; set; }


        public int EmployeeAge { get; set; }


        public string PositionName { get; set; }


        [DisplayName("Image")]
        public string? EmployeeImageUrl { get; set; }


    }
}
