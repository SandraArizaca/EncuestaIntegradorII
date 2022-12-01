using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace encuesta.Models
{
    public class TB_Pregunta
    {
        [Key]
        public int ID_Pregunta { set; get; }
        public int N_Seccion { set; get; }
        public int N_Orden { set; get; }
        public string C_Pregunta { set; get; }
        public string C_TipoPregunta { set; get; }
        public int N_Preguntas { set; get; }
        public int B_Obligatoria { set; get; }
        public int N_Ultimo { set; get; }
        public int N_TipoPregunta { set; get; }
        public int N_Estado { set; get; }
    }
}