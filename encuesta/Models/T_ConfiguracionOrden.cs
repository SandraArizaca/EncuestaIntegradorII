using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_ConfiguracionOrden
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { set; get; }
        [Key, Column(Order = 2)]
        public int ID_Seccion { set; get; }
        public int OrdenSec { set; get; }
        public int ID_Pregunta { set; get; }
        public int OrdenPreg { get; set; }
        public int ID_Alternativa { set; get; }
        public int OrdenAlter { get; set; }
    }
}