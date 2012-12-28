using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CTS.Expense.Domain
{
    public class Client
    {

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string ClientName { get; set; }

       // public ICollection<Client> Clients { get; set; }

    }


}
