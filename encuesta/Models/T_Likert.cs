using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_Likert
    {
        [Key]
        public int ID_Likert { set; get; }
        public string C_Likert { set; get; }
        public int N_Estado { set; get; }
    }
}