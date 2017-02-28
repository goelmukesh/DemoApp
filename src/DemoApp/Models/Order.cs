using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class Order
    {
        [Key]
        public int Ord_id { get; set; }

        [Display(Name="Order Date")]
        public DateTime Ord_date { get; set; }

        [Display(Name ="Amount")]
        public decimal ord_amt { get; set; }

        [Display(Name ="Customer Id")]
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
