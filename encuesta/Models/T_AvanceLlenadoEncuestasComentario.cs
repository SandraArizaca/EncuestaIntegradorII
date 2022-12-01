using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceLlenadoEncuestasComentario
    {
        [Key]
        public int ID_Encuesta { set; get; }
        public string C_Seccion { set; get; }
        public string C_Pregunta { set; get; }
        public string C_Alternativa { set; get; }
        public string C_Escala { set; get; }
        public string C_Ruc { set; get; }
        public string C_Usuario { set; get; }
        public string C_Comentario { set; get; }
    }
}