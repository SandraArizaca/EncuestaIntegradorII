using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace encuesta.Models
{
    public class TMP_Invitacion
    {
        [Key]
        public int ID { set; get; }
        public int? ID_Invitacion { set; get; }
        public string ID_Usuario { set; get; }
        public int? ID_PJuridica { set; get; }
        public string C_NombreCompleto { set; get; }
        public string C_Token { set; get; }
        public string C_Correo { set; get; }
        public string C_Ruc { set; get; }
        public string C_RazonSocial { set; get; }
        public string C_Direccion { set; get; }
    }
}