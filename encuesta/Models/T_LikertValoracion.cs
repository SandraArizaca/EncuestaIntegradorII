using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_LikertValoracion
    {
        [Key]
        public int ID_Valoracion { set; get; }
        public int N_Likert { set; get; }
        public string C_Valoracion { set; get; }
    }
}