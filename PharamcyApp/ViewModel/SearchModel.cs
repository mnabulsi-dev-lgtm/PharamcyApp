using PharamcyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharamcyApp.ViewModel
{
    public class SearchModel
    {
        public string PharmacyName { get; set; }
        public Cities PharmacyLocation { get; set; }

        public List<Pharmacy> pharmacies { get; set; }
    }
}
