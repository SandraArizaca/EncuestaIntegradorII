using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace encuesta.Models
{
    public class T_Avance
    {
        [Key]
        public int ID { set; get; }
        public string C_RazonSocial { set; get; }
        public string N_Usuario { set; get; }
        public string C_Estado { set; get; }
        public int? N_Finalizada { set; get; }
        public int? N_Proceso { set; get; }
    }
}