using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharamcyApp.Models
{
    public class Pharmacy
    {
        [Key]
        public int PharmacyID { get; set; }

        [Required(ErrorMessage ="The name of Pharamcy is Required")]
        [StringLength(20)]
        public string PharmacyName { get; set; }
        public Cities PharmacyLocation { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MinLength(7)]
        [MaxLength(10)]
        public string phonenumber { get; set; }

        public Status workhour { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Compare(nameof(email))]
        public string emailConf { get; set; }

        public byte[] CoverImage { get; set; } // data base 
        public string BackCoverImage { get; set; } // wwwroot

        //nav
        public List<Sell> sells { get; set; }

        
    }
}
