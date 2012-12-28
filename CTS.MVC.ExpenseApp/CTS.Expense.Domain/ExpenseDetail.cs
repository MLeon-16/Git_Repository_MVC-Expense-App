using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CTS.Expense.Domain
{
   public  class ExpenseDetail
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string ReceiptEntry { get; set; }

        public DateTime DateIncurred { get; set; }

        public int NumberOfGuest { get; set; }
        [StringLength(500)]
        public string GuestNames { get; set; }

        [Required, StringLength(200)]
        public string Establishment { get; set; }

        public string Reason { get; set; }
        public string Expensecategory { get; set; }
        public string Notes { get; set; }

        [Required]
        public double Amount { get; set; }
        public double AmountPerPerson { get; set; }

        [Required]
        public int ReportID { get; set; }
        public Report Report { get; set; }
    }
}
