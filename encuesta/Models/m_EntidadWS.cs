using System;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class m_EntidadWS
    {
        [Key]
        [Display(Name = "IdEntidad")]
        public string IdEntidad { get; set; }

        [Display(Name = "razonSocial")]
        public string razonSocial { get; set; }
    }
}