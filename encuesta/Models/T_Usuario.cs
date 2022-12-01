using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Usuario
    {
        [Key, Column(Order = 1)]
        public int ID_Usuario { get; set; }
        public int ID_Perfil { get; set;}
        public System.Nullable<int> ID_Publico { get; set; }
        public string C_Usuario { get; set; }
        public string C_Contrasena { get; set; }
        public string C_Nombres { get; set; }
        public string C_ApellidoPaterno { get; set; }
        public string C_ApellidoMaterno { get; set; }
        public string C_NombreCompleto { get; set; }
        public string C_Dni { get; set; }
        public string C_Ruc { get; set; }
        public string C_RazonSocial { get; set; }
        public string C_Tipo { get; set; }
        public string C_Ubigeo { get; set; }
        public string C_FechaCreacionLogistico { get; set; }
        public System.Nullable<int> N_Estado { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
        public string C_Token_Acceso { get; set; }
        public virtual T_Perfil T_Perfil { set; get; }

    }
}