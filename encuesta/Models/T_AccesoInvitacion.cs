using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace encuesta.Models
{
    public class T_AccesoInvitacion
    {
        [Key]
        public int ID_Invitacion { get; set; }
        public string C_Token { get; set; }
        public string C_RazonSocial { get; set; }
        public string C_NombreCompleto { get; set; }
        public string C_Correo { get; set; }
        public int ID_Area { get; set; }
        public int ID_Publico { get; set; }
    }
}