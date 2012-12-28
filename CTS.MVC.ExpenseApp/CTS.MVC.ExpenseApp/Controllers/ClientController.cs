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
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        public ActionResult Index()
        {
            // here is where we will mange the client and the Projects of that given client 
            // each client has a set of related projects 

            // need to get a list of all the clients :
            var clients = GetClients();
            //var clients = TestGetClients();
            return View(clients);
        }


       
        public ActionResult AddEditClient(string clientID, string clientName)
        {
            try
            {
                int intClientID = Convert.ToInt32(clientID);
                var db = new ExpenseDb();

                if (intClientID <= 0)
                {
                    // add a new client
                    var client = new Client { ClientName = clientName };
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                else// this is an edit of an existing Client
                {
                    db.Clients.Where(c => c.Id == intClientID).FirstOrDefault().ClientName = clientName;
                    //db change tracker
                    db.SaveChanges();
                   
                       
                    // how to update using Fluent syntax 
                   // db.Clients.Add(clinet);
                   // db.SaveChanges();

                }

                // get list of Clients 
                var clients = GetClients();

                return PartialView("_ClientTable", clients);

            }
            catch (Exception ex)
            {

                return ThrowJSONError(ex);
            }

        }

       
        public ActionResult DeleteClient(int clientID)
        {
            var db = new ExpenseDb();
            var client = db.Clients.Find(clientID);
            db.Clients.Remove(client);
            db.SaveChanges();
            
            // get list of Clients 
            var clients = GetClients();

            return PartialView("_ClientTable", clients);
        }

    
        public ActionResult GetClientProjects(int clientID)
        {
            var projects = getProjects(clientID);
           
        
            return PartialView("_ProjectTable", projects);
        }

        
        public ActionResult AddEditProject(int clientID, int projectID, string projectName)
        {
           try{
            var db = new ExpenseDb();

            if (projectID == 0)
            {
                // add a new project
                var project = new Project { ClientID = clientID, ProjectName = projectName };

                db.Project.Add(project);
                db.SaveChanges();
            }
            else// this is an edit of an existing Client
            {
               // db.Clients.Where(c => c.Id == intClientID).FirstOrDefault().ClientName = clientName;
                db.Project.Where(p => p.Id == projectID).FirstOrDefault().ProjectName = projectName;
                //db change tracker
                db.SaveChanges();

            }

            // get list of Projects
            var projects = getProjects(clientID);


            return PartialView("_ProjectTable", projects);
           }
           catch (Exception ex)
           {

               return ThrowJSONError(ex);
           }
        }

       
        public ActionResult DeleteProject(int projectID, int clientID) {
           
            var db = new ExpenseDb();
            var project = db.Project.Find(projectID);
            db.Project.Remove(project);
            db.SaveChanges();

            // get list of Clients 
            var projects = getProjects(clientID);


            return PartialView("_ProjectTable", projects);
        
        }

        
        //------------------------------Custom Methods-------------------------------------------
        // method to regenerate the list of Clients 
        private static List<ClientViewModel> GetClients()
        {
            ExpenseDb db = new ExpenseDb();
          

            var clients = db.Clients
                .OrderBy(n =>n.ClientName)
                .Select(x => new ClientViewModel()
                {
                    ClientName = x.ClientName,
                    ClientId = x.Id,
                    
                }).ToList();

        
            return clients;

        }
        // method used to get list of Projects
        private static List<ClientViewModel> getProjects(int clientID)
        {
            ExpenseDb db = new ExpenseDb();
            ClientViewModel viewModel = new ClientViewModel();

            var projects = db.Project
                .Where(x => x.ClientID == clientID)
                .Select(x => new ClientViewModel()

                {
                    ProjectId = x.Id,
                    ProjectName = x.ProjectName
                }).ToList();
            return projects;
        }
        // used to config the display so both pannels would show ------ Test Method 
        private static List<ClientViewModel> TestGetClients()
        {
            ExpenseDb db = new ExpenseDb();
            ClientViewModel viewModel = new ClientViewModel();

            var clients = db.Clients
                .Join(db.Project, a => a.Id, b => b.ClientID, (a, b) => new { a = a, b = b })
                .Where(x => x.a.Id == 1)
                .Select(x => new ClientViewModel() {
                ClientName = x.a.ClientName,
                ClientId = x.b.ClientID,
                ProjectName= x.b.ProjectName,
                ProjectId=x.b.Id
                }).ToList();
               
            return clients;
        }
        
        // this method is used to handel errors 
        protected JsonResult ThrowJSONError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            //Log your exception
            return Json(new { Message = e.Message });
        }

    }
}
