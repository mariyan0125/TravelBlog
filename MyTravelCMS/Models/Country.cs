using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTravelCMS.Models
{
    public class Country
    {
        [Key, ScaffoldColumn(false)]
        public int CountryID { get; set; }

        [Required, StringLength(255), Display(Name = "Name")]
        public string CountryName { get; set; }

        public virtual ICollection<Tip> Tips { get; set; }
        

    }
}