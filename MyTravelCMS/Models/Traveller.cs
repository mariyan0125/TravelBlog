using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTravelCMS.Models
{
    public class Traveller
    {
        [Key]
        public int TravellerID { get; set; }

        [Required, StringLength(255), Display(Name = "Name")]
        public string TravellerName { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Bio")]
        public string TravellerBio { get; set; }

       
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Tip> Tips { get; set; }
    }
}