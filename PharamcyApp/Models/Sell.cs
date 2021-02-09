using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharamcyApp.Models
{
    public class Sell
    {
        [Key]
        public int SellID { get; set; }
        public int MedicineID { get; set; }
        public int PharmacyID { get; set; }

        public Pharmacy Pharmacy { get; set; }
        public Medicine Medicine { get; set; }

    }
}
