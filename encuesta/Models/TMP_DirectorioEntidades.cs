using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TMP_DirectorioEntidades
    {
        [Key]
        public string C_RucEntidad { set; get; }
        public string C_NombreEntidad { set; get; }
        public string C_TipoEntidad { set; get; }
        public string C_DepartamentoEntidad { set; get; }
        public string C_IDUsuario { set; get; }
        public string C_NombreLogistico { set; get; }
        public string C_CorreoLogistico { set; get; }
        public int ID_UsuarioCarga { set; get; }
    }
}