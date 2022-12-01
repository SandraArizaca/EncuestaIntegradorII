using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class TMP_DirectorioProveedores
    {
        [Key]
        public string C_RucProveedor { set; get; }
        public string C_Proveedor { set; get; }
        public string C_TipoEmpresa { set; get; }
        public string C_DepartamentoProveedor { set; get; }
        public string C_IDUsuario { set; get; }
        public string C_Gestor { set; get; }
        public string C_CorreoProveedor { set; get; }
        public string C_CorreoUsuario { set; get; }
        public int ID_UsuarioCarga { set; get; }
    }
}