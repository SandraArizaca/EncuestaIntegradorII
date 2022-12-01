using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TB_TipoPregunta
    {
        [Key]
        public int ID_TipoPregunta { set; get; }
        public string C_TipoPregunta { set; get; }
        public int N_Estado { set; get; }
    }
}