using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_DetalleEncuesta
    {
        [Key, Column(Order = 1)]
        public int ID_Encuesta { set; get; }
        [Key, Column(Order = 2)]
        public int ID_Seccion { set; get; }
        [Key, Column(Order = 3)]
        public int ID_Pregunta { set; get; }
        [Key, Column(Order = 4)]
        public int ID_Alternativa { set; get; }
        public string C_Respuesta { get; set; }
        public int? N_Intervalo { set; get; }
        public string C_Comentario { get; set; }
    }
}