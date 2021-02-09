using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharamcyApp.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [Display(Prompt = "please enter the scientific name", Name = "Medicine Name")]
        [StringLength(20,MinimumLength =5)]
        [Column(TypeName ="nvarchar(20)")]
        public string MedicineName { get; set; }

        public des DeseaseType { get; set; }
       
        public prod productionCon { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)]
        public double price { get; set; }

        [DataType(DataType.Date)]
        public DateTime EXPDate { get; set; } = DateTime.Now;

        //Nav
        public List<Sell> sells { get; set; }
    }
}
