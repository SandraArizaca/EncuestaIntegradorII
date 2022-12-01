using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TMP_Pregunta
    {
        [Key]
        public int ID_Pregunta { set; get; }
        public int N_Orden { set; get; }
    }
}