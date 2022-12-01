using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class TMP_PreguntaAlter
    {
        [Key, Column(Order = 0)]
        public int ID_Pregunta { set; get; }
        public int? ID_Cuestionario { set; get; }
        public int N_Orden { set; get; }
        public string C_Pregunta { set; get; }
        public string C_TipoPregunta { set; get; }
        [Key, Column(Order = 1)]
        public int ID_Alternativa { set; get; }
        public string C_Alternativa { set; get; }
        public int? B_TieneComentario { set; get; }
        public string C_Secuencia { set; get; }
        public int? N_PreguntaSiguiente { set; get; }
        public int B_FinalizaEncuesta { set; get; }
    }
}