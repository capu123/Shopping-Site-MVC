using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "Max 2 digits")]
        [Required(ErrorMessage = "Enter your phone no starting 44")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter the House/Flat No")]
        [Display(Name = "House/Flat No")]
        public string HouseNo { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your postcode")]
        public string Postcode { get; set; }


        public bool GiftWrap { get; set; }
    }
}
