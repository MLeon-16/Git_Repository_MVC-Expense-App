using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace Contracts
{
    public class ClientDto
    {

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string ClientName { get; set; }


    }

    
}
