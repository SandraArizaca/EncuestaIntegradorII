using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceEncuestasDetalle
    {
        [Key, Column(Order = 1)]
        public int ID_Usuario { set; get; }
        public string C_Usuario { set; get; }
        public string C_NombreLogistico { set; get; }
        public string C_CorreoLogistico { set; get; }
        public string C_EstadoInvitacion { set; get; }
    }
}