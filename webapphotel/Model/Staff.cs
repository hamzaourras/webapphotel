using System.ComponentModel.DataAnnotations;

namespace webapphotel.Model
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should only contain letters.")]
        public string? Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone Number should be 10 digits.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public string? Address { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Monthly salary must be a positive number.")]
        [Display(Name = "Monthly Salary")]
        public decimal? MonthlySalary { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Hirement")]
        public DateTime? DateOfHirement { get; set; }

        [Display(Name = "Job Title")]
        public string? JobTitle { get; set; }

    }


}
