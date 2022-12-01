using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Invitacion
    {
        [Key]
        public int ID_Invitacion { set; get; }
        public int ID_Cuestionario { set; get; }
        public int ID_Usuario { set; get; }
        public int ID_Correo { set; get; }
        public int ID_Estado { set; get; }
        public System.Nullable<bool> N_Enviar { set; get; }
        public System.Nullable<bool> N_Reenviar { set; get; }
        public System.Nullable<int> N_Reiterativo { set; get; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }

        //[ForeignKey("ID_Cuestionario")]
        //public virtual T_Cuestionario T_Cuestionario { set; get; }
        //[ForeignKey("ID_Usuario")]
        //public virtual T_Usuario T_Usuario { set; get; }
    }
}