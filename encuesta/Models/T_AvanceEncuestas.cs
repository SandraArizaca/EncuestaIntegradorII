using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceEncuestas
    {
        [Key, Column(Order = 1)]
        public string C_Ruc { set; get; }
        public string C_RazonSocial { set; get; }
        public string C_Tipo { set; get; }
        public string C_Ubigeo { set; get; }
        public int N_Finalizada { set; get; }
        public int N_Valida { set; get; }
        public int N_Invalida { set; get; }
        public int N_Proceso { set; get; }
        public int N_SinRespuesta { set; get; }
        public int N_Invitaciones { set; get; }
    }
}