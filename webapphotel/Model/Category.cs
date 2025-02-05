using System.ComponentModel.DataAnnotations;

namespace webapphotel.Model
{
    public class Category
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
    }
}
