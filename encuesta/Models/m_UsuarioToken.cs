using System;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    //Clase de uso exclusivo con el DBContext para validacion de token
    public class m_UsuarioToken
    {
        [Key]
        [Display(Name = "ID Usuario")]
        public string ID_Usuario { get; set; }

        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        [Display(Name = "Persona Juridica")]
        public long? N_PersonaJuridica { get; set; }

        [Display(Name = "Persona")]
        public long? N_PJuridica_Persona { get; set; }

        [Display(Name = "Rol")]
        public long N_Rol { get; set; }

        [Display(Name = "Token Acceso")]
        public string C_Token_Acceso { get; set; }

        [Display(Name = "Expiracion Token")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> D_ExpiracionToken { get; set; }

        [Display(Name = "Observación")]
        public string C_Observacion { get; set; }

        [Display(Name = "Doc. Tramite")]
        public string C_Tramite { get; set; }

        [Display(Name = "Justificación")]
        public string C_Justificacion { get; set; }

        [Display(Name = "Estado")]
        public string C_Estado { get; set; }

        [Display(Name = "U. Crea Registro")]
        public string A_CreacionUsuario { get; set; }

        [Display(Name = "F. Creacion Registro")]
        public System.DateTime A_CreacionFecha { get; set; }

        [Display(Name = "U. Modifica Registro")]
        public string A_ModificacionUsuario { get; set; }

        [Display(Name = "F. Modificacion Registro")]
        public Nullable<System.DateTime> A_ModificacionFecha { get; set; }
    }
}