using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CTS.Expense.Domain
{
    public class Project
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string ProjectName { get; set; }

       
        public int ClientID { get; set; }
        public Client Client { get; set; }// - needs and object  of the foreign key table item to create the FK on the DB 


    }
}
