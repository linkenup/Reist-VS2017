using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reist_VS2017.Models
{
    public class Local
    {
        public string nome { get; set; }
        public string sigla { get; set; }
        public Endereco endereco { get; set; }
    }
}