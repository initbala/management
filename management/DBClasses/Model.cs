using System.ComponentModel.DataAnnotations;

namespace Management.DBClasses
{
    public class Employee
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, ErrorMessage = "Name Length is above 50 chars")]
        public string Name { get; set; } = "";
        public string Address { get; set; } = "default";
        [Required(ErrorMessage = "Employee Id is Required")]
        public string Id { get; set; } = "";
        [Required(ErrorMessage = "Joining Date is Required")]
        public string Joining { get; set; } = "";
        [Required(ErrorMessage = "Department is Required")]
        public string Department { get; set; } = "";
        [Required(ErrorMessage = "Salary is quired")]
        [Range(999, 99999, ErrorMessage = "Enter salary btween 999 to 99999")]
        public string Salary { get; set; } = "";
    }
    public class UserLogin
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "";

    }
    public class Department
    {
        public string Depid { get; set; } = "";
        public string Dep { get; set; } = "";
    }
}
