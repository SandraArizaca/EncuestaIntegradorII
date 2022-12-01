using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_Alternativa
    {
        [Key]
        public int ID_Alternativa { get; set; }
        public int ID_Pregunta { get; set; }
        public string C_Alternativa { get; set; }
        public int? N_Orden { get; set; }
        public System.Nullable<int> N_Intervalo { get; set; }
        //public bool? N_Comentario { get; set; }
        //public bool? N_ComentarioObligatorio { get; set; }
        //public bool? N_Secuencia { get; set; }
        //public bool? N_PregAnterior { get; set; }
        //public bool? N_PregSiguiente { get; set; }
        //public bool? N_FinalizaEncuesta { get; set; }
        public bool? N_Estado { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
        //public virtual List<T_Pregunta> T_Pregunta { set; get; }
    }
}