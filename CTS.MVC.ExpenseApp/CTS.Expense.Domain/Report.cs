using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS.Expense.Domain
{
    public class Report
    {

        public int Id { get; set; }// by convention Code first will set this as PK since it is name Id

        [Required, StringLength(50)]
        public string EmpName { get; set; }
        [Required, StringLength(50)] // this attribue will help guid the DB when generating the DB
        public string EmpTitle { get; set; }// employee's title 

        public DateTime MonthYear { get; set; }// ignore day and time

        public int? ErNumber { get; set; } // this prop is nullable : "?"
        public bool Billable { get; set; }

        
       // public int ProjectID { get; set; }// assuem that project is required 
        //public Project Project { get; set; }

        //public int ClientID { get; set; }
        //public Client Client { get; set; }// - needs and object  of the foreign key table item to create the FK on the DB 

       // public string ProjectName { get; set; } // this will be pulled using a LINQ Join 
        public int ProjectID { get; set; }
        public Project Project { get; set; }// - needs and object  of the foreign key table item to create the FK on the DB 

        public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; }

    }
}
