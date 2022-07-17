using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please enter your first name ...!")]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name ...!")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your address ...!")]
        [Display(Name = "Street Address")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your city ...!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public List<OrderDetail> orderDetails { get; set; }

        [ScaffoldColumn(false)]
        public double OrderTotal { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentType { get; set; }
        public string UserEmail { get; set; }
    }
}
