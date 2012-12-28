using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CTS.Expense.Business;
using CTS.Expense.Domain;
using CTS.MVC.ExpenseApp.Models;


namespace CTS.MVC.ExpenseApp.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index(int reportID)
        {
            try
            {

            
            var viewModel = new ExpenseDetailsViewModel();

            var db = new ExpenseDb();

            var establishments = GetEstablishments(db);// gets a list of Establishemnts 

            var report = GetReportDetails(reportID, db);// gets the details of a single reports 

            var expenses = GetReportExpenses(reportID, db);
            
           


           // ViewBag.ListOfEst = establishments;// this is putting a list of establishments in the View bag to then be retrieved from the JS
           
            viewModel.Establishments = establishments;
            viewModel.Report = report;
            viewModel.ListOfExpenses = expenses;
            if (report == null)
            {
                return View("Error");
            }
            else {
                return View(viewModel);
            }
            
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        
       
      [HttpPost]
        public ActionResult AddEditExpense(ExpenseDetailsViewModel viewModel)
        {
            var db = new ExpenseDb();
            // get expense ID if < 0 then it is a new report 
            if (viewModel.ExpenseInput.ExpenseID <= 0)
            {
                // this is a new expense 
                var expense = new ExpenseDetail
                {
                    DateIncurred = viewModel.ExpenseInput.DateIncurred,
                    ReceiptEntry = viewModel.ExpenseInput.ReceiptEntry,
                    NumberOfGuest = viewModel.ExpenseInput.NumberOfGuest,
                    GuestNames = viewModel.ExpenseInput.GuestNames,
                    Establishment = viewModel.ExpenseInput.Establishment,
                    Expensecategory = viewModel.ExpenseInput.Expensecategory,
                    Reason = viewModel.ExpenseInput.Reason,
                    Amount = viewModel.ExpenseInput.Amount,
                    AmountPerPerson = viewModel.ExpenseInput.AmountPerPerson,
                    ReportID = viewModel.ExpenseInput.ReportID,
                    Notes =viewModel.ExpenseInput.Notes

                };
                db.ExpenseDetail.Add(expense);
                db.SaveChanges();
                

            }
            else // it is an edit to an existing report
            {
                // update an expense 
                //db.ExpenseDetail.Where(e => e.Id == viewModel.ExpenseInput.ExpenseID).FirstOrDefault()
               // var expense = db.ExpenseDetail.Find(viewModel.ExpenseInput.ExpenseID);
                  
                    
               
               // db.Clients.Where(c => c.Id == intClientID).FirstOrDefault()
                var expense = db.ExpenseDetail.Where(e => e.Id == viewModel.ExpenseInput.ExpenseID).FirstOrDefault();
                expense.DateIncurred = viewModel.ExpenseInput.DateIncurred;
                expense.ReceiptEntry = viewModel.ExpenseInput.ReceiptEntry;
                expense.NumberOfGuest = viewModel.ExpenseInput.NumberOfGuest;
                expense.GuestNames = viewModel.ExpenseInput.GuestNames;
                expense.Establishment = viewModel.ExpenseInput.Establishment;
                expense.Expensecategory = viewModel.ExpenseInput.Expensecategory;
                expense.Reason = viewModel.ExpenseInput.Reason;
                expense.Notes = viewModel.ExpenseInput.Notes;
                expense.Amount = viewModel.ExpenseInput.Amount;
                expense.AmountPerPerson = viewModel.ExpenseInput.AmountPerPerson;
                expense.ReportID = viewModel.ExpenseInput.ReportID;
                //db.ExpenseDetail.Add(expense); when doing an update no need to do the add 
                
                db.SaveChanges();// just by doing the saveChanges it knows it is an update

                    
                    //.ClientName = clientName; 
                //db change tracker
                db.SaveChanges();
                    
                //db.Clients.Where(c => c.Id == intClientID).FirstOrDefault().ClientName = clientName;
                //db change tracker
                db.SaveChanges();
            
            }

            var establishments = GetEstablishments(db);// gets a list of Establishemnts 

            var report = GetReportDetails(viewModel.ExpenseInput.ReportID, db);// gets the details of a single reports 

            var expenses = GetReportExpenses(viewModel.ExpenseInput.ReportID, db);

            // ViewBag.ListOfEst = establishments;// this is putting a list of establishments in the View bag to then be retrieved from the JS

            viewModel.Establishments = establishments;
            viewModel.Report = report;
            viewModel.ListOfExpenses = expenses;
            if (report == null)
            {
                return View("Error");
            }
            else
            {
                return PartialView("_ExpenseDetails", viewModel);
                //return View("Index",viewModel);
            }

            
        }

        public ActionResult DeleteExpense(int expenseID, int reportID) {

            var viewModel = new ExpenseDetailsViewModel();

            var db = new ExpenseDb();

            // delete expense 
            var expense = db.ExpenseDetail.Find(expenseID);
            db.ExpenseDetail.Remove(expense);
            db.SaveChanges();


            // re-render Partial 
            var establishments = GetEstablishments(db);// gets a list of Establishemnts 

            var report = GetReportDetails(reportID, db);// gets the details of a single reports 

            var expenses = GetReportExpenses(reportID, db);




            // ViewBag.ListOfEst = establishments;// this is putting a list of establishments in the View bag to then be retrieved from the JS

            viewModel.Establishments = establishments;
            viewModel.Report = report;
            viewModel.ListOfExpenses = expenses;
            if (report == null)
            {
                return View("Error");
            }
            else {
                return PartialView("_ExpenseDetails", viewModel);
                //return View("Index", viewModel);
            }

           
        }

        ///////------------------------------------------- Establishment Actions : 
        public ActionResult AddEstablishment(string establishmentName)
        {
            // in this instance we dont have Access to the Id's so we will only compare the stirngs client side  
            // so we will always be creating a new Establishment

            var db = new ExpenseDb();

                var establishment = new Establishment { EstablishmentName = establishmentName };
                db.EstablishmentTBL.Add(establishment);

                db.SaveChanges();

           

            // here we return an updated list of the Establishments 
            var establishments = GetEstablishments(db);

            var viewModel = new ExpenseDetailsViewModel();
            viewModel.Establishments = establishments;

            return PartialView("_EstablishmentTxt",viewModel);
        }


        ///////---------------------------------- Establishment Actions : 

         
        /////////////////----------------------- Shared methods to repopulate page
        /// <summary>
        /// Gets the Expenses for a given expense 
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static List<ExpenseDetails> GetReportExpenses(int reportID, ExpenseDb db)
        {
            var expenses = db.ExpenseDetail
                .Where(e => e.ReportID == reportID)
                .Select(e => new ExpenseDetails
                {
                    
                    DateIncurred = e.DateIncurred,
                    ReceiptEntry= e.ReceiptEntry,
                    Establishment = e.Establishment,
                    Expensecategory = e.Expensecategory,
                    NumberOfGuest = e.NumberOfGuest,
                    GuestNames = e.GuestNames,
                    Amount = e.Amount,
                    AmountPerPerson = (e.Amount / (e.NumberOfGuest == 0 ? 1 : e.NumberOfGuest)),// here i get the amount per person server side 
                    Reason = e.Reason,
                    Notes = e.Notes,
                    ReportID = e.ReportID,
                    ExpenseID = e.Id
                }).ToList();

            return expenses;
        }

        /// <summary>
        /// Method used to get the list of common Establishemnts 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static List<EstablishmentInput> GetEstablishments(ExpenseDb db)
        {
            var establishments = db.EstablishmentTBL
                .OrderBy(e => e.EstablishmentName)
                .Select(e => new EstablishmentInput
                {
                    EstablishmentName = e.EstablishmentName,
                    // Id= e.Id
                }).ToList();
            return establishments;
        }

        /// <summary>
        ///  Method used to get a single reports's details 
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static ReportDisplay GetReportDetails(int reportID, ExpenseDb db)
        {
            var report = db.Reports
                .Where(r => r.Id == reportID)
                .Select(r => new ReportDisplay
                {
                    MonthYear = r.MonthYear,
                    ReportID = r.Id,
                    Billable = r.Billable,
                    EmpName = r.EmpName,
                    EmpTitle = r.EmpTitle,
                    ErNumber = r.ErNumber,
                    ProjectID = r.ProjectID,
                    ClientName = r.Project.Client.ClientName,
                    ProjectName = r.Project.ProjectName,// with in out report model there is an object of Project that is how it knows what project it is associated to 
                    Total = r.ExpenseDetails.Sum(e => e.Amount) != null ? r.ExpenseDetails.Sum(e => e.Amount) : 0.0,
                    numberOfExpenses = r.ExpenseDetails.Count

                }).FirstOrDefault();
            return report;
        }



    }

}
