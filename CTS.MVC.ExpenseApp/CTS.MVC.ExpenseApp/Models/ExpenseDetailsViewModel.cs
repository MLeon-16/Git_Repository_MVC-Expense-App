using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CTS.MVC.ExpenseApp.Models
{
    public class ExpenseDetailsViewModel
    {
        // these will be used for data input 
        public ExpenseDetails ExpenseInput { get; set; }// used for a single expense 
        public List<ExpenseDetails> ListOfExpenses { get; set; } // used for a list of Expenses


        // List to load Dynamiacly 
        public List<EstablishmentInput> Establishments { get; set; }

        // report that will be displayed 
        public ReportDisplay Report { get; set; } // 

    }


    public class ReportDisplay
    {

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

    }

    public class EstablishmentInput
    {
        // public int Id { get; set; }
        [Required, StringLength(200)]
        public string EstablishmentName { get; set; }
    }

    public class ExpenseDetails
    {

        public int ExpenseID { get; set; }
        [DisplayName("Receipt Entry")]
        [StringLength(50)]
        public string ReceiptEntry { get; set; }

        [DisplayName("Expense Date")]
        [Required]
        public DateTime DateIncurred { get; set; }

        [RegularExpression(@"^\d{1,2}", ErrorMessage = "number is invalid")]
        [DisplayName("# of Guest")]
        public int NumberOfGuest { get; set; }

        [DisplayName("GuestNames")]
        [StringLength(500)]
        public string GuestNames { get; set; }


        [Required, StringLength(200)]
        public string Establishment { get; set; }

        public string Reason { get; set; }
        [DisplayName("Expense Category")]
        [Required]
        public string Expensecategory { get; set; }
        public string Notes { get; set; }

        // what this Reg Ex is saying is it will accept any number of digits then a "." then 2 more digits 
        //"| OR" any number of digits with out the . or any other character 
        [DisplayName("Expense Amount")]
        [RegularExpression(@"^\d+[.]\d{1,2}|^\d+|^\d+[\.]", ErrorMessage = "Must be a valid monetary expression eg-19.95")]
        [Required]
        public double Amount { get; set; }

        [DisplayName("Amount Per Guest")]
        public double AmountPerPerson { get; set; }

        [Required]
        public int ReportID { get; set; }

    }
}