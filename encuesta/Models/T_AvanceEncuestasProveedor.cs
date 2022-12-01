using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_AvanceEncuestasProveedor
    {
        [Key, Column(Order = 1)]
        public int ID_Usuario { set; get; }
        public string C_Ruc { set; get; }
        public string C_RazonSocial { set; get; }
        public string C_TipoPersoneria { set; get; }
        public string C_Ubigeo { set; get; }
        public string C_Correo { set; get; }
        public string C_EstadoEncuesta { set; get; }
    }
}