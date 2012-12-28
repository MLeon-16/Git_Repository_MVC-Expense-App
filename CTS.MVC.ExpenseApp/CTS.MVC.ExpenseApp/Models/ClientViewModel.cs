using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.MVC.ExpenseApp.Models
{
    public class ClientViewModel
    {
        
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectCount { get; set; } 


            
        // Ask how to send these list to View instead of passing a quyer.List() 
        // how to validate using this View model 
        public List<ClientDetails> ClientList { get; set; }
        public List<ProjectDetails> ProjectList { get; set; }

       
    }

    // currently not in use
    public class ClientDetails 
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    
    }
    //currently not in use 
    public class ProjectDetails
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}