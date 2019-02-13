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

        //Raw page information (in Models/Page.cs)
        public virtual Tip Tip { get; set; }

        //need information about the different blogs this page COULD be
        //assigned to
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Traveller> Travellers { get; set; }


    }
}