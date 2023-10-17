using System.ComponentModel.DataAnnotations;

namespace CustomerInfo.Models
{
    public class CustomerDetailViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } 
    }
}
