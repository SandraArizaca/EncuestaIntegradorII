using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace encuesta.Models
{
    public class T_ClasificacionPregunta
    {
        [Key]
        public int ID_ClasificacionPregunta { set; get; }
        public string C_ClasificacionPregunta { set; get; }
        public System.Nullable<bool> N_Estado { set; get; }
        public string A_CreacionUsuario { set; get; }
        public System.Nullable<DateTime> A_CreacionFecha { set; get; }
        public string A_ModificacionUsuario { set; get; }
        public System.Nullable<DateTime> A_ModificacionFecha { set; get; }
    }
}