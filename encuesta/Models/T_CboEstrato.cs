using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_CboEstrato
    {
        [Key]
        public string ID_Estrato { get; set; }
        public string C_Estrato { get; set; }
    }
}