using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTravelCMS.Models;
using System.Data.SqlClient;

namespace MyTravelCMS.Controllers
{
    public class CountryController : Controller
    {
        private TravelCMSContext db = new TravelCMSContext();

        // GET: Country
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(string countryName)
        {
            string query = "insert into Countries (CountryName) " +
                "values (@name)";

            SqlParameter[] myparams = new SqlParameter[1];
            myparams[0] = new SqlParameter("@name", countryName);

            db.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            return View(db.Countries.Find(id));
        }

        public ActionResult Delete(int id)
        {
            //delete tips associated with the deleted country
           string query = "delete from tips where Country_CountryID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            //delete the country
            query = "delete from countries where CountryID = @id";
            param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
        
            return RedirectToAction("List");

        }

        public ActionResult List()
        {
            return View(db.Countries.ToList());
        }
    }
}
//the code may contain adopted and modified parts from inclass example as well as from online source https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/passing-arrays-as-arguments