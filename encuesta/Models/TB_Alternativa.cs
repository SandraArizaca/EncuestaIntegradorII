using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TB_Alternativa
    {
        [Key]
        public int ID_Alternativa { set; get; }
        public int N_Pregunta { set; get; }
        public string C_Alternativa { set; get; }
        public int N_Peso { set; get; }
        public int N_CantidadMinimaCaracteres { set; get; }
        public int N_CantidadMaximaCaracteres { set; get; }
        public int B_NuevoOrden { set; get; }
        public int? B_Comentario { set; get; }
        public int B_FinalizaEncuesta { set; get; }
        public string C_TipoPregunta { set; get; }
        public int N_TipoPregunta { set; get; }
    }
}