using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Muestreo
    {
        [Key]
        public int ID_Muestreo { get; set; }
        public int ID_Cuestionario { get; set; }
        public string C_Tipo { get; set; }
        public string C_Ubigeo { get; set; }
        public int N_Cantidad1 { get; set; }
        public int N_Cantidad2 { get; set; }
        public int N_Cantidad3 { get; set; }
        public string C_TipoMuestreo { get; set; }
        public string C_DescMuestreo { get; set; }
    }
}