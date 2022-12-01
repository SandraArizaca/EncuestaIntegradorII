using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceMuestreo
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { set; get; }
        [Key, Column(Order = 2)]
        public string C_Tipo { set; get; }
        [Key, Column(Order = 3)]
        public string C_Ubigeo { set; get; }
        public int N_Muestra { set; get; }
        public int N_EncuestaValida { set; get; }
        public int N_MarcoMuestralValido { set; get; }
        public int N_TotalEncuestasFinalizadas { set; get; }
    }
}