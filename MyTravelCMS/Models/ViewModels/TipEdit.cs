using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTravelCMS.Models.ViewModels
{
    public class TipEdit
    {
        public TipEdit()
        {

        }

       
        public virtual Tip Tip { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Traveller> Travellers { get; set; }


    }
}