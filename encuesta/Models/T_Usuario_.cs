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
        //[Key]
        //public string ID_Usuario { get; set; }
        //public string C_Contrasena { get; set; }
        //public int N_Rol { get; set; }
        //public string C_Token_Acceso { get; set; }
        ////public int N_PJuridica { get; set; }
        //public int N_Estado { get; set; }

        [Key, Column(Order = 1)]
        public string ID_Usuario { get; set; }
        public string C_Contrasena { get; set; }
        public int N_Rol { get; set; }
        public string C_Token_Acceso { get; set; }
        public System.Nullable<DateTime> D_ExpiracionToken { get; set; }
        public int N_Estado { get; set; }
        [Key, Column(Order = 2)]
        public int N_PJuridica { get; set; }
        public string C_NroDocumento { get; set; }
        public string C_ApellidoPaterno { get; set; }
        public string C_ApellidoMaterno { get; set; }
        public string C_Nombres { get; set; }
        public string C_NombreCompleto { get; set; }
        public string C_Correo { get; set; }

        public virtual ICollection<T_Invitacion> Invitaciones { get; set; }
    }
}