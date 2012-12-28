using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CTS.MVC.ExpenseApp.Models;
using CTS.Expense.Business;
using CTS.Expense.Domain;

namespace CTS.MVC.ExpenseApp.Controllers
{
    public class ReportsListController : Controller
    {
        //
        // GET: /ReportsList/

        public ActionResult Index()
        {
            // generate a list of all the expense Reports 

            // send that list as a model to the view 

            var db = new ExpenseDb();

            //var query = from r in db.Reports
            //            join p in db.Project on r.ProjectID equals p.Id
            //            join ed in db.ExpenseDetail on r.Id equals ed.ReportID
            //            group new {r,p,ed} by new {r.MonthYear,r.Billable, p.ProjectName}into g
            //            select new ReportDetails
            //            {
            //                MonthYear = g.Key.MonthYear,
            //                Billable = g.Key.Billable,
            //                ProjectName= g.Key.ProjectName,
            //                Total = g.Sum(a=> a.ed.Amount)
            //            };

            var reports = GetReports(db);

            //Reports.Select(r => new { r.Id, r.EmpName, r.MonthYear, Total = r.ReportExpenseDetails.Sum(e => e.Amount) });

            var clients = db.Clients

                .Select(c => new ClientListDetails
                {
                    ClientID = c.Id,
                    ClientName = c.ClientName
                }).ToList();

            // empty prj list, os the view will not get a null ref exception 
            var tempPrjList = db.Project
            .Select(p => new ProjectListDetails
            {
            }).ToList();






            ReportViewModel model = new ReportViewModel();

            model.Reports = reports;
            model.Clients = clients;
            model.Projects = tempPrjList;


            return View("Index", model);
        }



        
       /// <summary>
       /// returns the list of the selected Clients projects used to populate a Drop down list 
       /// -- update both the Client and proejct DLL are passed in to the view model in order to keep 
       /// </summary>
       /// <param name="clientID"></param>
       /// <returns></returns>
        public ActionResult GetProjects(int clientID)
        {
            ExpenseDb db = new ExpenseDb();

            ReportViewModel viewModel = new ReportViewModel();

            var clients = db.Clients

                .Select(c => new ClientListDetails
                {
                    ClientID = c.Id,
                    ClientName = c.ClientName
                }).ToList();

            var projects = db.Project
                .Where(x => x.ClientID == clientID)
                .Select(x => new ProjectListDetails

                {
                    ProjectID = x.Id != null ? x.Id : -1,
                    ProjectName = x.ProjectName != null ? x.ProjectName: "No Projects Found"
                }).ToList();

            viewModel.Projects = projects;
            viewModel.Clients = clients;
            viewModel.SelectedClientID = clientID;


            return PartialView("_ProjectDDL", viewModel);
        }

        public ActionResult AddEditReport(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // do good stuff
            }
            else
            {
                // make sure you return the bad model to the view... otherwise the model errors get wiped out
                // and your validation helpers don't work
            }
            var db = new ExpenseDb();
            if (model.ReportID > 0)
            {
                // this is an update 
                //db.EstablishmentTBL.Where(e => e.Id == establishmentID).FirstOrDefault().EstablishmentName = establishmentName;
               var report= db.Reports.Where(e => e.Id == model.ReportID).FirstOrDefault();
               report.MonthYear = model.MonthYearInput;
               report.Billable = model.BillableInput;
               report.EmpName = model.EmpNameInput;
               report.EmpTitle = model.EmpTitleInput;
               report.ErNumber = model.ErNumberInput != null ? model.ErNumberInput : -1;
               report.ProjectID = model.SelectedProjectID;
                
                    
                db.SaveChanges();

            }
            else {
                // this is a new report 
                var report = new Report { Billable = model.BillableInput, EmpName = model.EmpNameInput, EmpTitle = model.EmpTitleInput, MonthYear = (DateTime)model.MonthYearInput, ErNumber = model.ErNumberInput != null ? model.ErNumberInput : 0, ProjectID = model.SelectedProjectID };
                db.Reports.Add(report);
                db.SaveChanges();
            
            }
           
            // add a new client
            // var client = new Client { ClientName = clientName };
            // db.Clients.Add(client);

            


            var reports = GetReports(db);

            model.Reports = reports;


            return PartialView("_ReportsTable", model);
        }

        /// <summary>
        /// this is the method removes a given report from the DB
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        public ActionResult DeleteReport(int reportID)
        {
            var model = new ReportViewModel();
            var db = new ExpenseDb();

            //delete the give report by report ID 
            var report = db.Reports.Find(reportID);
            db.Reports.Remove(report);
            db.SaveChanges();

            var reports = GetReports(db);

            model.Reports = reports;
            return PartialView("_ReportsTable", model);
        }

        public ActionResult GetReportDetailsForEdit(int projectID)
        {

            ExpenseDb db = new ExpenseDb();

            ReportViewModel viewModel = new ReportViewModel();

            var projectValues = db.Project
                .Where(p => p.Id == projectID)
                .Select(p => new SelectedReportDetails
                {
                     ClientID = p.ClientID,
                     ClientName = p.Client.ClientName,
                     ProjectID = p.Id != null ? p.Id : -1,
                     ProjectName = p.ProjectName != null ? p.ProjectName : "No Projects Found"
                }).SingleOrDefault();



            /// gets the list of all the projects and clients, since they are both in the same partial view they both need to be loaded  
            var projects = db.Project
               .Where(x => x.ClientID == projectValues.ClientID)
               .Select(x => new ProjectListDetails

               {
                   ProjectID = x.Id,
                   ProjectName = x.ProjectName
               }).ToList();

            var clients = db.Clients

               .Select(c => new ClientListDetails
               {
                   ClientID = c.Id,
                   ClientName = c.ClientName
               }).ToList();

            /// add the elements to the View Model 
            viewModel.Projects = projects;
            viewModel.Clients = clients;
            viewModel.SelectedClientID = projectValues.ClientID;
            viewModel.SelectedClientName = projectValues.ClientName;
            viewModel.SelectedProjectID = projectValues.ProjectID;
            viewModel.SelectedProjectName = projectValues.ProjectName;
           
            


            return PartialView("_ProjectDDL", viewModel);
        }
        /// <summary>
        /// This method is used to get a list of all the expenses
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static List<ReportDetails> GetReports(ExpenseDb db)
        {
            var reports = db.Reports

                .Select(r => new ReportDetails
                {
                    ReportID = r.Id,
                    MonthYear = r.MonthYear,
                    Billable = r.Billable,
                    EmpName = r.EmpName,
                    EmpTitle = r.EmpTitle,
                    ErNumber = r.ErNumber,
                    ProjectID = r.ProjectID,
                    ProjectName = r.Project.ProjectName,// with in out report model there is an object of Project that is how it knows what project it is associated to 
                    Total = r.ExpenseDetails.Sum(e => e.Amount) != null ? r.ExpenseDetails.Sum(e => e.Amount) : 0.0,
                    numberOfExpenses = r.ExpenseDetails.Count 
                    //Score = (int?)(from v in e.Votes
                    //where v.VoterID == voterID
                    //select v.Score).FirstOrDefault()

                }).ToList();
            return reports;
        }

        ///------------------------------------------------------------------- Establishment Functions :
        //
        public ActionResult EstablishmentView()
        {
            // Est are just used as an auto fill not a FK of any table 

            var db = new ExpenseDb();
            EstablishmentViewModel model = GetEstablishments(db);

            return View(model);

        }




        /// <summary>
        /// This action will be used to send a new/eddited establishment to the DB
        /// </summary>
        /// <param name="establishmentName"></param>
        /// <param name="establishmentID"></param>
        /// <returns></returns>
        public ActionResult AddEditEstablishment(string establishmentName, int establishmentID)
        {
            var db = new ExpenseDb();

            if (establishmentID <= 0) /// we add a new est to the DB
            {
                var establishment = new Establishment { EstablishmentName = establishmentName };
                db.EstablishmentTBL.Add(establishment);
                db.SaveChanges();

            }
            else // we edit the Est on the DB
            {
                db.EstablishmentTBL.Where(e => e.Id == establishmentID).FirstOrDefault().EstablishmentName = establishmentName;
                db.SaveChanges();

            }


            // here we return an updated list of the Establishments 
            EstablishmentViewModel model = GetEstablishments(db);

            return PartialView("_EstablishmentTable", model);
        }

        /// <summary>
        /// This action will remove a given Est. from the DB 
        /// </summary>
        /// <param name="establishmentID"></param>
        /// <returns> an updated list of the EST. </returns>
        public ActionResult DeleteEstablishment(int establishmentID)
        {
            var db = new ExpenseDb();

            var establishment = db.EstablishmentTBL.Find(establishmentID);
            db.EstablishmentTBL.Remove(establishment);
            db.SaveChanges();


            // here we return an updated list of the Establishments 
            EstablishmentViewModel model = GetEstablishments(db);

            return PartialView("_EstablishmentTable", model);
        }
        // returns a list of all the establishemtns in a Model object
        private EstablishmentViewModel GetEstablishments(ExpenseDb db)
        {
            var est = from e in db.EstablishmentTBL
                      orderby e.EstablishmentName
                      select new EstablishmentDetails
                      {
                          EstablishmentName = e.EstablishmentName,
                          Id = e.Id
                      };

            EstablishmentViewModel model = new EstablishmentViewModel();
            model.Establishments = est.ToList();
            return model;
        }


    }
}
