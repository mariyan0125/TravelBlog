using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTravelCMS.Models
{
    public class Tip
    {
        [Key, ScaffoldColumn(false)]
        public int TipID { get; set; }

        [Required, StringLength(255), Display(Name = "Title")]
        public string TipTitle { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Content")]
        public string TipContent { get; set; }

        public int HasImg { get; set; }
        public string ImgType { get; set; }


        public virtual Country Country { get; set; }
        public virtual Traveller Traveller { get; set; }
    }
}