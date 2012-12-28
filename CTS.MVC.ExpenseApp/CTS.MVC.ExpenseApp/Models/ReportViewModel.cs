using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using CTS.Expense.Domain;

namespace CTS.MVC.ExpenseApp.Models
{
    public class ReportViewModel
    {

        public List<ReportDetails> Reports { get; set; }

        public List<ClientListDetails> Clients { get; set; }

        public List<ProjectListDetails> Projects { get; set; }

        public List<string> EmpTitles { get; set; }

        //   //SELECTED DROP DOWN VALUES
        public int SelectedClientID { get; set; }
        public string SelectedClientName { get; set; }
        public int SelectedProjectID { get; set; }
        public string SelectedProjectName { get; set; }

        // these are the items that will used on an edit :        public string EmpName { get; set; }
        public int ReportID { get; set; }
        [Required, StringLength(50)] // this attribue will help guid the DB when generating the DB
        public string EmpTitleInput { get; set; }// employee's title 
        public string EmpNameInput { get; set; }
        [DisplayName("Report Month/Year")]
        [Required]
        [RegularExpression(@"^\d{1,2}[-/]\d{4}")]
        public DateTime MonthYearInput { get; set; }// ignore day and time
        public int? ErNumberInput { get; set; } // this prop is nullable : "?"
        public bool BillableInput { get; set; }
        public int ProjectIDInput { get; set; }

       

    }

    public class ReportDetails {

        public int ReportID { get; set; }
        [Required, StringLength(50)]
        public string EmpName { get; set; }
        [Required, StringLength(50)] // this attribue will help guid the DB when generating the DB
        public string EmpTitle { get; set; }// employee's title 
        public DateTime MonthYear { get; set; }// ignore day and time
        public int? ErNumber { get; set; } // this prop is nullable : "?"
        public bool Billable { get; set; }

        public double Total { get; set; }

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }

        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public int numberOfExpenses { get; set; }
        public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; }
        public virtual ICollection<Client> Client { get; set; }
 
    }

    public class ClientListDetails { 
        public string ClientName { get; set; } 
        public int ClientID { get; set; } 
    }

    public class ProjectListDetails {
        public string ProjectName { get; set; }
        public int ProjectID { get; set; }
    }

    public class SelectedReportDetails {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
    
    }

}