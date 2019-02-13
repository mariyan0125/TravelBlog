using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTravelCMS.Models;
using MyTravelCMS.Models.ViewModels;
using System.IO;

namespace MyTravelCMS.Controllers
{
    public class TipController : Controller
    {
        private TravelCMSContext db = new TravelCMSContext();
        // GET: Tip
        public ActionResult Index()
        { //make List in view
            return RedirectToAction("List");         
        }

        public ActionResult New()
        {
            TipEdit tipEditView = new TipEdit();
            tipEditView.Countries = db.Countries.ToList();
            tipEditView.Travellers = db.Travellers.ToList();

            return View(tipEditView);
        }

        [HttpPost]
        public ActionResult Create(string TipTitle_New, string TipContent_New, int TipTraveller_New, int? TipCountry_New)
        {
            string query = "insert into Tips (TipTitle, TipContent, HasImg, ImgType, Country_CountryID, Traveller_TravellerID ) " +
                "values (@title,@content, 0, 0, @cid, @tid)";

            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@title", TipTitle_New);
            myparams[1] = new SqlParameter("@content", TipContent_New);
            myparams[2] = new SqlParameter("@cid", TipCountry_New);
            myparams[3] = new SqlParameter("@tid", TipTraveller_New);

            db.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {//don't forget to make Details in view
            string query = "select * from tips where tipid =@id";

            return View(db.Tips.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        public ActionResult Edit(int? id)
        {
            //compound view that holds the data to display tip edit         
            TipEdit tipEdit = new TipEdit();
            tipEdit.Tip = db.Tips.Find(id);
            tipEdit.Countries = db.Countries.ToList();
            tipEdit.Travellers = db.Travellers.ToList();
            if (tipEdit.Tip != null) return View(tipEdit);
            else return HttpNotFound();
        }
        //edit
        [HttpPost]
        public ActionResult Edit(int? id, string TipTitle, string TipContent, int? TipCountry, int? TipTraveller, HttpPostedFileBase tipImg)
        {
            if (tipImg != null || tipImg?.ContentLength > 0)
            {
                //this part adopted from inclass example 
                //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                var extension = Path.GetExtension(tipImg.FileName).Substring(1);

                if (valtypes.Contains(extension))
                {
                    string imgStr = id + "." + extension;

                    string path = Path.Combine(Server.MapPath("~/images/UploadedFiles"), imgStr);

                    tipImg.SaveAs(path);

                    int HasImg = 1;
                    string ImgType = extension;

                    string pictureQuery = "update tips set HasImg=@hasImg, ImgType=@type where tipid=@id";
                    SqlParameter[] picParams = new SqlParameter[3];
                    picParams[0] = new SqlParameter("@hasImg", HasImg);
                    picParams[1] = new SqlParameter("@type", ImgType);
                    picParams[2] = new SqlParameter("@id", id);

                    db.Database.ExecuteSqlCommand(pictureQuery, picParams);

                }
            }



            if ((id == null) || (db.Tips.Find(id) == null))
            {
                return HttpNotFound();

            }
          
            string query = "update tips set tiptitle=@title, " +
                "tipcontent=@content, " +
                "Country_CountryID=@cid, " + "Traveller_TravellerID=@tid where tipid = @id";
            SqlParameter[] parameters = new SqlParameter[5];
            
            parameters[0] = new SqlParameter("@title", TipTitle);
            parameters[1] = new SqlParameter("@content", TipContent);
            parameters[2] = new SqlParameter("@cid", TipCountry);
            parameters[3] = new SqlParameter("@tid", TipTraveller);
            parameters[4] = new SqlParameter("@id", id);

            
            
            db.Database.ExecuteSqlCommand(query, parameters);


            return RedirectToAction("Details/" + id);
        }
    
        public ActionResult Delete(int id)
        {
            string query = "delete from tips where TipID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
             
            return RedirectToAction("List");

        }

        public ActionResult List()
        { 
            return View(db.Tips.ToList());
        }
    }
}
//the code contains adopted and modified parts from inclass example as well as from online source https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/passing-arrays-as-arguments