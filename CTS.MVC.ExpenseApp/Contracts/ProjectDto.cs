using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    class ProjectDto
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string ProjectName { get; set; }


        public int ClientID { get; set; }
    }
}
