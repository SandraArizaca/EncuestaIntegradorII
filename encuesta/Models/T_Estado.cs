using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_Estado
    {
        [Key]
        public int ID_Estado { get; set; }
        public System.Nullable<int> ID_EstadoPadre { get; set; }
        public string C_Tabla { get; set; }
        public string C_Estado { get; set; }
        public System.Nullable<bool> N_Estado { get; set; }
    }
}