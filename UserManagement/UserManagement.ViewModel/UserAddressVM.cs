using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.ViewModel
{
    public class UserAddressVM
    {
        public int UId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* User Name between 5 and 50 character in length.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "* Designation between 5 and 50 character in length.")]
        public string Designation { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        public string ImagePath { get; set; }

        public AddressVM Address { get; set; }
    }
    public class UserUpdateAddressVM
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int UId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* User Name between 2 and 50 character in length.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "* Designation between 2 and 50 character in length.")]
        public string Designation { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        public string ImagePath { get; set; }

        public AddressVM Address { get; set; }
    }
    public class AddressVM
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* Country between 2 and 50 character in length.")]
        public string Country { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* State between 2 and 50 character in length.")]
        public string State { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* City between 2 and 50 character in length.")]
        public string City { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* PinCode between 2 and 50 character in length.")]
        public string PinCode { get; set; }
        public string Street { get; set; }
    }
}
