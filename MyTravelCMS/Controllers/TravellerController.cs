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
    public class TravellerController : Controller
    {
        private TravelCMSContext db = new TravelCMSContext();

        // GET: Traveller
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //make List in view
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string travellerName, string travellerBio)
        {
            string query = "insert into Travellers (TravellerName, TravellerBio) " +
                "values (@name,@bio)";

            SqlParameter[] myparams = new SqlParameter[2];
            myparams[0] = new SqlParameter("@name", travellerName);
            myparams[1] = new SqlParameter("@bio", travellerBio);

            db.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            return View(db.Travellers.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int? id, string travellerName, string travellerBio)
        {
            if ((id == null) || (db.Travellers.Find(id) == null))
            {
                return HttpNotFound();

            }
            string query = "update travellers set TravellerName=@name, TravellerBio=@bio where travellerid=@id";
            SqlParameter[] myparams = new SqlParameter[3];
            myparams[0] = new SqlParameter("@name", travellerName);
            myparams[1] = new SqlParameter("@bio", travellerBio);
            myparams[2] = new SqlParameter("@id", id);

             db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");

        }

        public ActionResult Details(int id)
        {
            return View(db.Travellers.Find(id));
        }

        public ActionResult Delete(int id)
        {
            string query = "delete from travellers where TravellerID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            
            return RedirectToAction("List");

        }

        public ActionResult List()
        {
           
            return View(db.Travellers.ToList());

        }
    }
}//
//the code may contain adopted and modified parts from inclass example as well as from online source https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/passing-arrays-as-arguments