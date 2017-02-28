using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer Id")]
        public int CustId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        public string CustName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Phone Number is required")]
        [RegularExpression(@"^[1-9][0-9]{9}$", ErrorMessage = "PhoneNumber should contain only numbers and must be 10 digits long")]
        [Phone]
        [Display(Name ="Phone Number")]
        public long contact { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
