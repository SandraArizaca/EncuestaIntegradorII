using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TB_Clasificacion
    {
        [Key]
        public int ID_Clasificacion { set; get; }
        public string C_Clasificacion { set; get; }
        public int N_Estado { set; get; }
    }
}