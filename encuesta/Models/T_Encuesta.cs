using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Encuesta
    {
        [Key]
        public int ID_Encuesta { set; get; }
        public int ID_Cuestionario { set; get; }
        public int ID_Usuario { set; get; }

        public System.Nullable<bool> N_Reenviar { set; get; }
        public int ID_Estado { set; get; }
        public System.Nullable<DateTime> D_FechaCierre { set; get; }
        public System.Nullable<DateTime> D_FechaApertura { set; get; }
        public string C_Token { get; set; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }

        //[ForeignKey("ID_Cuestionario, ID_Usuario")]
        //public virtual T_Invitacion T_Invitacion { set; get; }
        //[ForeignKey("ID_Cuestionario")]
        //public virtual T_Config_Cuestionario T_Config_Cuestionario { set; get; }

        //public virtual T_EncuestaDetalle T_EncuestaDetalle { set; get; }
    }
}