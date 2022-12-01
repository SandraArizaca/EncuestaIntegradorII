using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_PreguntaSecuencia
    {
        [Key]
        public int ID_Secuencia { set; get; }
        public int N_Alternativa { set; get; }
        public int N_Pregunta { set; get; }
        public int B_FinSecuencia { set; get; }
    }
}