using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_PJuridica
    {
        [Key]
        public int ID_PJuridica { set; get; }
        public string C_Ruc { set; get; }
        public int N_TipoPJuridica { set; get; }
        public string C_RazonSocial { set; get; }
        public string C_Direccion { set; get; }
        public string C_Referencia { set; get; }
        public string C_Telefono { set; get; }
        public string C_Fax { set; get; }
        public int N_Estado { set; get; }

        public virtual ICollection<T_Invitacion> Invitaciones { get; set; }
    }
}