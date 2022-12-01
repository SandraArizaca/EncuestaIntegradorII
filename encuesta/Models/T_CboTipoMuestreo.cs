using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class T_CboTipoMuestreo
    {
        [Key]
        public string ID_TipoMuestreo { get; set; }
        public string C_Muestreo { get; set; }
    }
}