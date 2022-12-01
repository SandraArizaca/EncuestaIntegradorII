using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Directorio
    {
        [Key, Column(Order = 1)]
        public int ID_Cuestionario { set; get; }
        [Key, Column(Order = 2)]
        public int ID_Usuario { set; get; }
        public int N_Enviar { set; get; }
        public string C_Ruc { set; get; }
        public string C_RazonSocial { get; set; }
        public string C_Tipo { get; set; }
        public string C_Ubigeo { get; set; }
        public string C_Usuario { get; set; }
        public string C_NombreCompleto { get; set; }
        [Key, Column(Order = 3)]
        public int ID_Correo { set; get; }
        public string C_Correo { get; set; }
        public int N_Reenviar { set; get; }
        public int Est_Encuesta { set; get; }
        public int Est_Invitacion { set; get; }
        public string C_EstadoI { set; get; }
    }
}