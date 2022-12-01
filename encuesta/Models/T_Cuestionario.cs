using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_Cuestionario
    {
        [Key]
        public int ID_Cuestionario { set; get; }
        public int ID_Area { set; get; }
        public int ID_Publico { set; get; }
        public string C_Cuestionario { set; get; }
        public string C_Presentacion { set; get; }
        public string C_Resumen { set; get; }
        public int N_NroPreguntas { set; get; }
        public DateTime D_FechaInicio { set; get; }
        [DataType(DataType.Date)]
        public DateTime D_FechaFinal { set; get; }
        public string C_Ano { set; get; }
        public string C_MensajeFinal { set; get; }
        public int ID_Estado { set; get; }
        public string A_CreacionUsuario { get; set; }
        public System.Nullable<DateTime> A_CreacionFecha { get; set; }
        public string A_ModificacionUsuario { get; set; }
        public System.Nullable<DateTime> A_ModificacionFecha { get; set; }
        [DataType(DataType.EmailAddress)]
        public string C_CorreoRemitente { get; set; }
        public virtual T_Area T_Area { get; set; }
        public virtual T_Publico T_Publico { set; get; }
        public virtual T_Estado T_Estado { set; get; }
    }
}