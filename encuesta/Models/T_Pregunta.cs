using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace encuesta.Models
{
    public class T_Pregunta
    {
        [Key]
        public int ID_Pregunta { get; set; }
        public int ID_ClasificacionPregunta { get; set; }
        public int ID_TipoPregunta { get; set; }
        public string C_Pregunta { get; set; }
        public System.Nullable<int> N_NivelValoracion { get; set; }
        public System.Nullable<bool> N_Estado { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
        public virtual T_ClasificacionPregunta T_Clasificacion { get; set; }
        public virtual T_TipoPregunta T_TipoPregunta { get; set; }
    }
}