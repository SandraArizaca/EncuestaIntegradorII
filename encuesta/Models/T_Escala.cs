using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encuesta.Models
{
    public class T_Escala
    {
        [Key]
        public int ID_Escala { set; get; }
        public int ID_TipoLikert { set; get; }
        public int ID_Nivel { set; get; }
        public int N_Intervalo { set; get; }
        public string C_Escala { set; get; }
        public bool N_Estado { set; get; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }

        [ForeignKey("ID_TipoLikert")]
        public virtual T_TipoLikert T_TipoLikert { get; set; }
    }
}