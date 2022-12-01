using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_CboEstado
    {
        [Key]
        public int ID_Estado { get; set; }
        public string C_Estado { get; set; }
    }
}