using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CTS.Expense.Domain;
using CTS.Expense.Business;

namespace CTS.MVC.ExpenseApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            var db = new ExpenseDb();// this is our access to the data base 

            var reports = db.Reports.ToArray();

           ViewBag.Message = string.Format("You have {0} reports", reports.Length);

            return View();
           
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
