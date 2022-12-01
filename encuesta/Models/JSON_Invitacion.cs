using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class JSON_Invitacion
    {
        [Key]
        public string N_Usuario { set; get; }
        public int? N_PJuridica { set; get; }
        public string C_Token { set; get; }
    }
}