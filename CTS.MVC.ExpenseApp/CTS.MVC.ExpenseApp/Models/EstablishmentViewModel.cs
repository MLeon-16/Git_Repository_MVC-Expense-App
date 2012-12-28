using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;

namespace CTS.MVC.ExpenseApp.Models
{
    public class EstablishmentViewModel
    {
        public List<EstablishmentDetails> Establishments { get; set; }
    }

    public class EstablishmentDetails {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string EstablishmentName { get; set; }
    }
}