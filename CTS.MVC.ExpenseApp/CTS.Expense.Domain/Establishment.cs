using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CTS.Expense.Domain
{
    public class Establishment
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string EstablishmentName { get; set; }
    }
}
