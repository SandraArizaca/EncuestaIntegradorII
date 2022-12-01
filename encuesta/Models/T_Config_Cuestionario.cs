using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Config_Cuestionario
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { get; set; }
        [Key, Column(Order = 2)]
        public int ID_Seccion { get; set; }
        [Key, Column(Order = 3)]
        public int ID_Pregunta { get; set; }
        [Key, Column(Order = 4)]
        public int ID_Alternativa { get; set; }
        public System.Nullable<int> N_OrdenSeccion { get; set; }
        public System.Nullable<int> N_OrdenPreg { get; set; }
        public System.Nullable<int> N_Orden { get; set; }
        public System.Nullable<int> N_Intervalo { get; set; }
        public System.Nullable<int> ID_TipoLikert { get; set; }
        public System.Nullable<bool> N_PregEmisionBoleto { get; set; }
        public System.Nullable<bool> N_PregNoIncluyeEmisionBoleto { get; set; }
        public System.Nullable<bool> N_PregSecuencia { get; set; }
        public System.Nullable<bool> N_PregComentario { get; set; }
        public System.Nullable<bool> N_PregComentarioObligatorio { get; set; }
        public System.Nullable<int> N_PregAnterior { get; set; }
        public System.Nullable<int> N_PregSiguiente { get; set; }
        public System.Nullable<bool> N_AltComentario { get; set; }
        public System.Nullable<bool> N_AltComentarioObligatorio { get; set; }
        public System.Nullable<bool> N_AltSecuencia { get; set; }
        public System.Nullable<int> N_AltPregAnterior { get; set; }
        public System.Nullable<int> N_AltPregSiguiente { get; set; }
        public System.Nullable<bool> N_FinalizaEncuesta { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
        public System.Nullable<bool> N_AltEmisionBoleto { get; set; }
        public virtual T_Cuestionario T_Cuestionario { get; set; }
        public virtual T_Seccion T_Seccion { get; set; }
        public virtual T_Pregunta T_Pregunta { get; set; }
        public virtual T_Alternativa T_Alternativa { get; set; }
        public virtual T_TipoLikert T_TipoLikert { get; set; }
    }
}