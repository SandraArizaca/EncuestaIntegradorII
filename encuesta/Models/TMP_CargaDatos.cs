using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class TMP_CargaDatos
    {
        [Key, Column(Order = 1)]
        public string C_Ruc { set; get; }
        public string C_RazonSocial { set; get; }
        public string C_Tipo { set; get; }
        public string C_Publico { set; get; }
        public string C_Ubigeo { set; get; }
        public string C_IDUsuario { set; get; }
        [Key, Column(Order = 2)]
        public string C_NombreCompleto { set; get; }
        public string C_Correo { set; get; }
        public string C_Correo2 { set; get; }
    }
}