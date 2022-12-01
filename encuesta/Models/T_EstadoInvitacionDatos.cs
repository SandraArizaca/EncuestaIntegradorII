using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_EstadoInvitacionDatos
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { get; set; }
        [Key, Column(Order = 2)]
        public string C_Tipo { get; set; }
        [Key, Column(Order = 3)]
        public string C_Ubigeo { get; set; }
        public int N_InvitacionEnviada { get; set; }
        public int N_InvitacionNoEnviada { get; set; }
        public int N_InvitacionError { get; set; }
        public int N_MarcoMuestral { get; set; }
    }
}