using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_MVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter the employee name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please select the employee gender")]
        public char EmployeeGender { get; set; }

        public DateTime EmployeeBirthDate { get; set; }

        public int FK_PositionId { get; set; }

        public decimal EmployeeSalary { get; set; }

        public string EmployeeMobileNumber { get; set; }

        public int EmployeeAge { get; set; }

        public string PositionName { get; set; }

        public string EmployeeImageUrl { get; set; }

    }
}
