using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_ConfigAlternativaEscala
    {
        [Key, Column(Order = 1)]
        public int ID_Alternativa { get; set; }
        [Key, Column(Order = 2)]
        public int ID_Escala { get; set; }
        public int ID_Pregunta { get; set; }
        public System.Nullable<bool> N_Comentario { get; set; }
        public System.Nullable<bool> N_ComentarioObligatorio { get; set; }
        public System.Nullable<bool> N_Estado { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }

        public virtual T_Escala T_Escala { set; get; }
    }
}