using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_TipoPregunta
    {
        [Key]
        public int ID_TipoPregunta { set;  get; }
        public string C_TipoPregunta { set;  get; }
        public bool N_Estado { set;  get; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
    }
}