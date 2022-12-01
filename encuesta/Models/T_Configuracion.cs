using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Configuracion
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { get; set; }
        [Key, Column(Order = 2)]
        public int ID_Seccion { get; set; }
        [Key, Column(Order = 3)]
        public int ID_Pregunta { get; set; }
        [Key, Column(Order = 4)]
        public int ID_Alternativa { get; set; }
        [Key, Column(Order = 5)]
        public int ID_TipoLikert { get; set; }
        [Key, Column(Order = 6)]
        public int ID_Escala { get; set; }
        [Key, Column(Order = 7)]
        public int ID_Nivel { get; set; }
        public System.Nullable<int> N_OrdenSeccion { get; set; }
        public System.Nullable<int> N_OrdenPreg { get; set; }
        public System.Nullable<int> N_Orden { get; set; }
        public System.Nullable<int> N_Intervalo { get; set; }
        public System.Nullable<int> N_IntervaloE { get; set; }
        public System.Nullable<int> N_PregEmisionBoleto { get; set; }
        public System.Nullable<int> N_PregNoIncluyeEmisionBoleto { get; set; }
        public System.Nullable<int> N_PregComentario { get; set; }
        public System.Nullable<int> N_PregComentarioObligatorio { get; set; }
        public System.Nullable<int> N_PregSecuencia { get; set; }
        public System.Nullable<int> N_PregAnterior { get; set; }
        public System.Nullable<int> N_PregSiguiente { get; set; }
        public System.Nullable<int> N_AltEmisionBoleto { get; set; }
        public System.Nullable<int> N_AltComentario { get; set; }
        public System.Nullable<int> N_AltComentarioObligatorio { get; set; }
        public System.Nullable<int> N_AltSecuencia { get; set; }
        public System.Nullable<int> N_AltPregAnterior { get; set; }
        public System.Nullable<int> N_AltPregSiguiente { get; set; }
        public System.Nullable<int> N_FinalizaEncuesta { get; set; }
        public System.Nullable<int> N_Comentario { get; set; }
        public System.Nullable<int> N_ComentarioObligatorio { get; set; }
        public System.Nullable<int> ID_Encuesta { get; set; }
        public System.Nullable<int> ID_AlternativaR { get; set; }
        public string C_Respuesta { get; set; }
        public System.Nullable<int> N_IntervaloR { get; set; }
        public string C_Comentario { get; set; }
        
        public virtual T_Cuestionario T_Cuestionario { get; set; }
        public virtual T_Seccion T_Seccion { get; set; }
        public virtual T_Pregunta T_Pregunta { get; set; }
        public virtual T_Alternativa T_Alternativa { get; set; }
        public virtual T_TipoLikert T_TipoLikert { get; set; }
        public virtual T_Escala T_Escala { get; set; }
        public virtual T_Encuesta T_Encuesta { get; set; }
    }
}