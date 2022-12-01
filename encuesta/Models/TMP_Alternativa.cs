using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TMP_Alternativa
    {
        [Key]
        public int ID_Encuesta { set; get; }
        public int N_Invitacion { set; get; }
        public int ID_Invitacion { set; get; }
        public int N_Cuestionario { set; get; }
        public int N_Pregunta { set; get; }
        public int N_Peso { set; get; }
        public int C_Comentario { set; get; }



    }
}