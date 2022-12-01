using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace encuesta.Models
{
    public class TMP_Encuesta
    {
        [Key]
        public int ID_Alternativa { set; get; }
        public string C_Alternativa { set; get; }
        public int ID_Seccion { set; get; }
        public int N_Cuestionario { set; get; }
        public string C_Seccion { set; get; }
        public int ID_Pregunta { set; get; }
        public int N_Orden { set; get; }
        public string C_Pregunta { set; get; }
        public int N_TipoPregunta { set; get; }
        public int? N_Likert { set; get; }
        public int B_TieneComentario { set; get; }
        public int N_PreguntaBifurca { set; get; }
        public int N_Peso { set; get; }
        public int B_FinalizaEncuesta { set; get; }
        public int B_NuevoOrden { set; get; }
        public int B_SoloEstaOpcionNuevoOrden { set; get; }
        public int B_LikertComentario { set; get; }

        public string C_Presentacion { set; get; }
        public string C_Metodo { set; get; }
    }
}