using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceLlenadoEncuestas
    {
        [Key, Column(Order = 1)]
        public int ID_Seccion { set; get; }
        public string C_Seccion { set; get; }
        [Key, Column(Order = 2)]
        public int ID_Pregunta { get; set; }
        public string C_Pregunta { get; set; }
        [Key, Column(Order = 3)]
        public int ID_Alternativa { get; set; }
        public string C_Alternativa { get; set; }
        [Key, Column(Order = 4)]
        public int ID_Escala { set; get; }
        public string C_Escala { set; get; }
        public int N_Respuestas { set; get; }
        public int N_Validas { set; get; }
        public int N_Comentarios { set; get; }
        public int N_Comentario { set; get; }
    }
}