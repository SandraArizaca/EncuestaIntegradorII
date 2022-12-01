using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using encuesta.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;
//using encuesta.DTO;

namespace encuesta.Contexto
{
    public class CTX_Encuesta : DbContext
    {
        public DbSet<T_Cuestionario> T_Cuestionario { get; set; }
        public DbSet<T_Area> T_Area { get; set; }
        //public DbSet<T_Clasificacion> T_Clasificacion { set; get; }
        //public DbSet<T_ClasificacionPregunta> T_ClasificacionPregunta { get; set; }
        public DbSet<T_Publico> T_Publico { get; set; }
        //public DbSet<T_TipoPregunta> T_TipoPregunta { get; set; }
        //public DbSet<T_TipoLikert> T_TipoLikert { set; get; }
        //public DbSet<T_Escala> T_Escala { set; get; }
        //public DbSet<T_LikertValoracion> T_LikertValoracion { set; get; }
        //public DbSet<T_ConfigAlternativaEscala> T_ConfigAlternativaEscala { get; set; }
        public DbSet<T_Estado> T_Estado { get; set; }
        //public DbSet<T_CboEstado> T_CboEstado { get; set; }
        //public DbSet<T_CboTipoMuestreo> T_CboTipoMuestreo { get; set; }
        //public DbSet<T_CboEstrato> T_CboEstrato { get; set; }

        //public DbSet<T_Orden> T_Orden { set; get; }
        //public DbSet<TMP_PreguntaAlter> TMP_PreguntaAlter { set; get; }
        //public DbSet<T_Identificador> T_Identificador { set; get; }
        //public DbSet<TMP_Invitacion> TMP_Invitacion { set; get; }
        //public DbSet<JSON_Invitacion> JSON_Invitacion { set; get; }
        //public DbSet<T_Avance> T_Avance { set; get; }
        //public DbSet<NewID> NewID { set; get; }

        //public DbSet<TB_Clasificacion> TB_Clasificacion { set; get; }
        //public DbSet<TB_TipoPregunta> TB_TipoPregunta { set; get; }
        //public DbSet<TB_Pregunta> TB_Pregunta { set; get; }
        //public DbSet<TB_Alternativa> TB_Alternativa { set; get; }

        //public DbSet<TB_bk1> TB_bk1 { set; get; }

        //public DbSet<T_Pregunta> T_Pregunta { get; set; }



        //public DbSet<T_Invitacion> T_Invitacion { set; get; }
        //public DbSet<T_Encuesta> T_Encuesta { set; get; }
        //public DbSet<T_DetalleEncuesta> T_DetalleEncuesta { set; get; }
        //public DbSet<T_Seccion> T_Seccion { get; set; }
        //public DbSet<T_Alternativa> T_Alternativa { set; get; }

        //public DbSet<T_Directorio> T_Directorio { set; get; }
        //public DbSet<T_PreguntaSecuencia> T_PreguntaSecuencia { set; get; }
        //public DbSet<TMP_Encuesta> TMP_Encuesta { set; get; }
        //public DbSet<TMP_Pregunta> TMP_Pregunta { set; get; }
        //public DbSet<TMP_Alternativa> TMP_Alternativa { set; get; }

        //public DbSet<m_EntidadWS> m_EntidadWS { set; get; }

        //public DbSet<T_Usuario> T_Usuario { get; set; }
        //public DbSet<T_Config_Cuestionario> T_Config_Cuestionario { get; set; }
        //public DbSet<T_Configuracion> T_Configuracion { get; set; }
        //public DbSet<T_ConfiguracionOrden> T_ConfiguracionOrden { get; set; }
        //public DbSet<T_Muestreo> T_Muestreo { get; set; }
        ////public DbSet<T_PJuridica> T_PJuridica { set; get; }

        //public DbSet<TMP_DirectorioEntidades> TMP_DirectorioEntidades { get; set; }
        //public DbSet<TMP_DirectorioProveedores> TMP_DirectorioProveedores { get; set; }
        //public DbSet<TMP_CargaDatos> TMP_CargaDatos { get; set; }
        //public DbSet<T_AvanceEncuestas> T_AvanceEncuestasEntidad { get; set; }
        //public DbSet<T_AvanceEncuestasDetalle> T_AvanceEncuestasEntidadDetalle { get; set; }
        //public DbSet<T_AvanceEncuestasProveedor> T_AvancceEncuestasProveedor { get; set; }
        //public DbSet<T_AvanceLlenadoEncuestas> T_AvanceLlenadoEncuestas { get; set; }
        //public DbSet<T_AvanceLlenadoEncuestasComentario> T_AvanceLlenadoEncuestasComentarios { get; set; }
        //public DbSet<T_EstadoInvitacionDatos> T_EstadoInvitacionDatos { get; set; }
        //public DbSet<T_AccesoInvitacion> T_AccesoInvitacion { get; set; }

        //public DbSet<T_AvanceMuestreo> T_AvanceMuestreo { get; set; }
        //public DbSet<T_UsuarioAdministracion> T_UsuarioAdministracion { set; get; }

        public int SqlFlag = 0; //0=Normal, 1=error
        public string SqlMsg = string.Empty;
        public string SqlMsgInicio = string.Empty;

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    modelBuilder.Entity<T_Cuestionario>()
        //            .HasRequired<T_Estado>(s => s.T_Estado)
        //            .WithMany(s => s.t_cuestionarios)
        //            .HasForeignKey(s => s.N_Estado);

        //    modelBuilder.Entity<T_Cuestionario>()
        //            .HasRequired<T_Publico>(s => s.T_Publico)
        //            .WithMany(s => s.t_cuestionarios)
        //            .HasForeignKey(s => s.N_Publico);

        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<T_Config_Cuestionario>().HasKey(table => new
        //    {
        //        table.ID_Cuestionario,
        //        table.ID_Seccion
        //    });
        //}

        //public T_Usuario ValidaLogin(string usuario, string contrasena)
        //{
        //    List<T_Usuario> lstUsuario;
        //    T_Usuario Resultado = new T_Usuario();
        //    Resultado.ID_Usuario = 0;
        //    SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", usuario);
        //    string password = EncriptaCadena(string.Concat(usuario.ToUpper(), contrasena));
        //    SqlParameter ContrasenaParameter = new SqlParameter("@Contrasena", password);
        //    //SqlParameter ContrasenaParameter = new SqlParameter("@Contrasena", contrasena);
        //    try
        //    {
        //        lstUsuario = this.T_Usuario.SqlQuery("[dbo].[PA_Usuario_Login] @ID_Usuario, @Contrasena", IDUsuarioParameter, ContrasenaParameter).ToList();

        //        if (lstUsuario.Count > 0)
        //        {
        //            Resultado = lstUsuario.ElementAt(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SqlMsg = ex.Message;
        //    }
        //    return Resultado;
        //}
        public string[] ValidarToken(string C_token, string C_RutaRecurso, int N_Rol)
        {
            string[] mensaje = { "-1", "Usuario Inactivo o Sesion expiró" };
            SqlParameter TokenParameter = new SqlParameter("@C_Token_Acceso", C_token);
            SqlParameter RutaRecursoParameter = new SqlParameter("@C_RutaRecurso", C_RutaRecurso);
            SqlParameter RolParameter = new SqlParameter("@N_Rol", N_Rol);

            try
            {
                mensaje = Database.SqlQuery<string>("[dbo].[PA_ValidaToken] @C_Token_Acceso, @C_RutaRecurso,@N_Rol", TokenParameter, RutaRecursoParameter, RolParameter).First().Split('^');
            }
            catch (Exception ex)
            {
                //mensaje[0] = "-1";
                //mensaje[1] = ex.Message;
            }

            return mensaje;
        }
        public string[] cerrarSesion(string C_token)
        {
            string[] mensaje = { "-1", "Ocurrió un error!!" };
            SqlParameter TokenParameter = new SqlParameter("@C_Token_Acceso", C_token);

            try
            {
                mensaje = Database.SqlQuery<string>("[dbo].[PA_InactivarToken] @C_Token_Acceso", TokenParameter).First().Split('^');
            }
            catch (Exception ex)
            {
                mensaje[0] = "-1";
                mensaje[1] = ex.Message;
            }

            return mensaje;
        }

        private String EncriptaCadena(String cadena)
        {
            string hashString = string.Empty;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(cadena);
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(bytes);
                foreach (byte x in hash)
                {
                    hashString += String.Format("{0:x2}", x);
                }
            }
            catch (Exception e)
            {

            }
            return hashString;
        }

        //public int EliminarConfiguracion(int IdCuestionario, int IdSeccion)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", IdCuestionario);
        //    SqlParameter IDSeccionParameter = new SqlParameter("@ID_Seccion", IdSeccion);
        //    List<T_Config_Cuestionario> lst;
        //    try
        //    {
        //        lst = this.T_Config_Cuestionario.SqlQuery("[dbo].[PA_ConfigCuestionario_Elimina] @ID_Cuestionario, @ID_Seccion", IDCuestionarioParameter, IDSeccionParameter).ToList();
        //        return 1;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return 0;
        //}

        //public int EliminarConfiguracion(int IdCuestionario, int IdSeccion, int IdPregunta)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", IdCuestionario);
        //    SqlParameter IDSeccionParameter = new SqlParameter("@ID_Seccion", IdSeccion);
        //    SqlParameter IDPreguntaParameter = new SqlParameter("@ID_Pregunta", IdPregunta);
        //    List<T_Config_Cuestionario> lst;
        //    try
        //    {
        //        lst = this.T_Config_Cuestionario.SqlQuery("[dbo].[PA_ConfigCuestionarioPregunta_Elimina] @ID_Cuestionario, @ID_Seccion, @ID_Pregunta", IDCuestionarioParameter, IDSeccionParameter, IDPreguntaParameter).ToList();
        //        return 1;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return 0;
        //}

        //public int EliminaCargaDatos(int IdUsuario)
        //{
        //    SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", IdUsuario);
        //    List<TMP_CargaDatos> lst;
        //    try
        //    {
        //        lst = this.TMP_CargaDatos.SqlQuery("[dbo].[PA_CargaDatos_Elimina] @ID_Usuario", IDUsuarioParameter).ToList();
        //        return 1;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return 0;
        //}

        //public List<T_Seccion> ListConfigCuestionarioSecciones(int IdCuestionario)
        //{
        //    List<T_Seccion> lstResultado = new List<T_Seccion>();
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", IdCuestionario);
        //    try
        //    {
        //        lstResultado = this.T_Seccion.SqlQuery("[dbo].[PA_ConfigCuestionarioSeccion_List] @ID_Cuestionario", IDCuestionarioParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }

        //    return lstResultado;
        //}

        //public List<T_ConfigAlternativaEscala> ListConfigAlternativaEscala(int IdPregunta, int IdAlternativa, int IdTipoLikert)
        //{
        //    List<T_ConfigAlternativaEscala> lstResultado = new List<T_ConfigAlternativaEscala>();
        //    SqlParameter IDPreguntaParameter = new SqlParameter("@ID_Pregunta", IdPregunta);
        //    SqlParameter IDAlternativaParameter = new SqlParameter("@ID_Alternativa", IdAlternativa);
        //    SqlParameter IDTipoLikertParameter = new SqlParameter("@ID_TipoLikert", IdTipoLikert);

        //    try
        //    {
        //        lstResultado = this.T_ConfigAlternativaEscala.SqlQuery("[dbo].[PA_ConfigAlternativaEscala_List] @ID_Pregunta,@ID_Alternativa,@ID_TipoLikert", IDPreguntaParameter, IDAlternativaParameter, IDTipoLikertParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }

        //    return lstResultado;
        //}

        //public List<T_Configuracion> ListConfigDetalleEncuesta(int IdCuestionario, int IdEncuesta, string CToken)
        //{
        //    List<T_Configuracion> lstResultado = new List<T_Configuracion>();
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", IdCuestionario);
        //    SqlParameter IDEncuestaParameter = new SqlParameter("@ID_Encuesta", IdEncuesta);
        //    SqlParameter CTokenParameter = new SqlParameter("@C_Token", CToken);

        //    try
        //    {
        //        lstResultado = this.T_Configuracion.SqlQuery("[dbo].[PA_ConfigDetalleEncuesta_List] @ID_Cuestionario,@ID_Encuesta,@C_Token", IDCuestionarioParameter, IDEncuestaParameter, CTokenParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }

        //    return lstResultado;
        //}

        //public List<T_Directorio> ListDirectorio(int IdCuestionario, int proc, string CTipo, string CUbigeo, int IdEstadoE)
        //{
        //    List<T_Directorio> lstResultado = new List<T_Directorio>();
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", IdCuestionario);
        //    SqlParameter ProcesoParameter = new SqlParameter("@Proc", proc);
        //    SqlParameter IDEstadoEParameter = new SqlParameter("@ID_EstadoE", IdEstadoE);
        //    SqlParameter CTipoParameter = new SqlParameter("@CTipo", CTipo);
        //    SqlParameter CUbigeoParameter = new SqlParameter("@CUbigeo", CUbigeo);

        //    try
        //    {
        //        lstResultado = this.T_Directorio.SqlQuery("[dbo].[PA_Directorio_List] @ID_Cuestionario, @Proc, @ID_EstadoE, @CTipo, @CUbigeo", IDCuestionarioParameter, ProcesoParameter, IDEstadoEParameter, CTipoParameter, CUbigeoParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }

        //    return lstResultado;
        //}

        //public List<TMP_CargaDatos> ListCargaDatos(int IdUsuario)
        //{
        //    List<TMP_CargaDatos> lstResultado = new List<TMP_CargaDatos>();
        //    SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", IdUsuario);

        //    try
        //    {
        //        lstResultado = this.TMP_CargaDatos.SqlQuery("[dbo].[PA_CragaDatos_List] @ID_Usuario", IDUsuarioParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }

        //    return lstResultado;
        //}

        //public int EliminarAlternativa(int IdAlternativa)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IdAlternativa", IdAlternativa);
        //    List<T_Alternativa> lst;
        //    try
        //    {
        //        lst = this.T_Alternativa.SqlQuery("[dbo].[PA_Alternativa_Elimina] @IdAlternativa", IDCuestionarioParameter).ToList();
        //        return 1;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return 0;
        //}

        //public void InsertarAlternativasPregunta(int idCuestionario, int idSeccion, int idPregunta)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IDCuestionario", idCuestionario);
        //    SqlParameter IDSeccionParameter = new SqlParameter("@IDSeccion", idSeccion);
        //    SqlParameter IDPreguntaParameter = new SqlParameter("@IDPregunta", idPregunta);
        //    List<T_Config_Cuestionario> lst;
        //    try
        //    {
        //        lst = this.T_Config_Cuestionario.SqlQuery("[dbo].[PA_InsertarAlternativas] @IDCuestionario,@IDSeccion,@IDPregunta", IDCuestionarioParameter, IDSeccionParameter, IDPreguntaParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //}

        //public void InsertarAltTipoLikert(int idCuestionario, int idSeccion, int idPregunta, int idTipoLikert)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IDCuestionario", idCuestionario);
        //    SqlParameter IDSeccionParameter = new SqlParameter("@IDSeccion", idSeccion);
        //    SqlParameter IDPreguntaParameter = new SqlParameter("@IDPregunta", idPregunta);
        //    SqlParameter IDTipoLikertParameter = new SqlParameter("@IDTipoLikert", idTipoLikert);
        //    List<T_Config_Cuestionario> lst;
        //    try
        //    {
        //        lst = this.T_Config_Cuestionario.SqlQuery("[dbo].[PA_InsertarTipoLikertAlternativas] @IDCuestionario,@IDSeccion,@IDPregunta,@IDTipoLikert", IDCuestionarioParameter, IDSeccionParameter, IDPreguntaParameter, IDTipoLikertParameter).ToList();
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //}

        public void InsertarConfigEscala(int ID_Alternativa, int ID_Pregunta, string C_ComentarioEscala, string C_ComentariOblgEscala, string C_Usuario)
        {
            var ar = C_ComentarioEscala.Split(',');
            var ar2 = C_ComentariOblgEscala.Split(',');
            var nComentario = 0;
            var nComentarioOblg = 0;
            var idEscala = 0;

            for (int i = 0; i < (ar.Length - 1); i++)
            {
                //var cIdComentario = ar[i].ToString();
                var cRow = ar[i].ToString();
                var arRow = cRow.Split(':');
                var cId = arRow[0].ToString();
                var cVal = arRow[1].ToString();

                //var exist = C_ComentariOblgEscala.IndexOf(cIdComentario);

                //if(exist > 0)
                //{
                //    nComentarioOblg = 1;
                //}
                //else
                //{
                //    nComentarioOblg = 0;
                //}

                if (cVal == "true")
                {
                    nComentario = 1;
                } else
                {
                    nComentario = 0;
                }
                idEscala = Convert.ToInt32(cId);

                if (idEscala > 0)
                {
                    SqlParameter IDAlternativaParameter = new SqlParameter("@IDAlternativa", ID_Alternativa);
                    SqlParameter IDEscalaParameter = new SqlParameter("@IDEscala", idEscala);
                    SqlParameter IDPreguntaParameter = new SqlParameter("@IDPregunta", ID_Pregunta);
                    SqlParameter NValParameter = new SqlParameter("@NVal", nComentario);
                    SqlParameter NProcParameter = new SqlParameter("@NProc", 1);
                    SqlParameter CUsuarioParameter = new SqlParameter("@Usuario", C_Usuario);
                //    List<T_ConfigAlternativaEscala> lst;
                //    try
                //    {
                //        lst = this.T_ConfigAlternativaEscala.SqlQuery("[dbo].[PA_InsertarConfigEscala] @IDAlternativa,@IDEscala,@IDPregunta,@NVal,@NProc,@Usuario", IDAlternativaParameter, IDEscalaParameter, IDPreguntaParameter, NValParameter, NProcParameter, CUsuarioParameter).ToList();
                //    }
                //    catch (SqlException Ex)
                //    {
                //        SqlFlag = 2;
                //        SqlMsg = Ex.Message;
                //    }
                }
            }

            for (int i = 0; i < (ar2.Length - 1); i++)
            {
                //var cIdComentario = ar[i].ToString();
                var cRow2 = ar2[i].ToString();
                var arRow2 = cRow2.Split(':');
                var cId2 = arRow2[0].ToString();
                var cVal2 = arRow2[1].ToString();

                //var exist = C_ComentariOblgEscala.IndexOf(cIdComentario);

                //if(exist > 0)
                //{
                //    nComentarioOblg = 1;
                //}
                //else
                //{
                //    nComentarioOblg = 0;
                //}

                if (cVal2 == "true")
                {
                    nComentarioOblg = 1;
                }
                else
                {
                    nComentarioOblg = 0;
                }
                idEscala = Convert.ToInt32(cId2);

                if (idEscala > 0)
                {
                    SqlParameter IDAlternativaParameter = new SqlParameter("@IDAlternativa", ID_Alternativa);
                    SqlParameter IDEscalaParameter = new SqlParameter("@IDEscala", idEscala);
                    SqlParameter IDPreguntaParameter = new SqlParameter("@IDPregunta", ID_Pregunta);
                    SqlParameter NValParameter = new SqlParameter("@NVal", nComentarioOblg);
                    SqlParameter NProcParameter = new SqlParameter("@NProc", 2);
                    SqlParameter CUsuarioParameter = new SqlParameter("@Usuario", C_Usuario);
                    //List<T_ConfigAlternativaEscala> lst;
                    //try
                    //{
                    //    lst = this.T_ConfigAlternativaEscala.SqlQuery("[dbo].[PA_InsertarConfigEscala] @IDAlternativa,@IDEscala,@IDPregunta,@NVal,@NProc,@Usuario", IDAlternativaParameter, IDEscalaParameter, IDPreguntaParameter, NValParameter, NProcParameter, CUsuarioParameter).ToList();
                    //}
                    //catch (SqlException Ex)
                    //{
                    //    SqlFlag = 2;
                    //    SqlMsg = Ex.Message;
                    //}
                }
            }

            //SqlParameter IDCuestionarioParameter = new SqlParameter("@IDCuestionario", idCuestionario);
            //SqlParameter IDSeccionParameter = new SqlParameter("@IDSeccion", idSeccion);
            //SqlParameter IDPreguntaParameter = new SqlParameter("@IDPregunta", idPregunta);
            //SqlParameter IDTipoLikertParameter = new SqlParameter("@IDTipoLikert", idTipoLikert);
            //List<T_Config_Cuestionario> lst;
            //try
            //{
            //    lst = this.T_Config_Cuestionario.SqlQuery("[dbo].[PA_InsertarTipoLikertAlternativas] @IDCuestionario,@IDSeccion,@IDPregunta,@IDTipoLikert", IDCuestionarioParameter, IDSeccionParameter, IDPreguntaParameter, IDTipoLikertParameter).ToList();
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
        }

        public void InsertarInvitacionUsuarios(int idCuestionario, int idUsuario)
        {
            SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", idCuestionario);
            SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", idUsuario);
            //List<TMP_CargaDatos> lst;
            //try
            //{
            //    lst = this.TMP_CargaDatos.SqlQuery("[dbo].[PA_InvitacionUsuario_Ins] @ID_Cuestionario,@ID_Usuario", IDCuestionarioParameter, IDUsuarioParameter).ToList();
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
        }

        public void RegistrarUsuarioInvitacion(int idCuestionario, int idPublico, int idUsuario, string C_Ruc, string C_RazonSocial, string C_Tipo, string C_Ubigeo, string C_TipoDocumento, string C_NroDocumento, string C_NombreCompleto, string C_Correo)
        {
            SqlParameter IDCuestionarioParameter = new SqlParameter("@ID_Cuestionario", idCuestionario);
            SqlParameter IDPublicoParameter = new SqlParameter("@ID_Publico", idPublico);
            SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", idUsuario);
            SqlParameter RucParameter = new SqlParameter("@Ruc", C_Ruc);
            SqlParameter RazonSocialParameter = new SqlParameter("@RazonSocial", C_RazonSocial);
            SqlParameter TipoParameter = new SqlParameter("@Tipo", C_Tipo);
            SqlParameter UbigeoParameter = new SqlParameter("@Ubigeo", C_Ubigeo);
            SqlParameter TipoDocumentoParameter = new SqlParameter("@TipoDocumento", C_TipoDocumento);
            SqlParameter NroDocumentoParameter = new SqlParameter("@NroDocumento", C_NroDocumento);
            SqlParameter NombreCompletoParameter = new SqlParameter("@NombreCompleto", C_NombreCompleto);
            SqlParameter CorreoParameter = new SqlParameter("@Correo", C_Correo);

            //List<TMP_CargaDatos> lst;
            //try
            //{
            //    lst = this.TMP_CargaDatos.SqlQuery("[dbo].[PA_RegistraInvitacionUsuario_Ins] @ID_Cuestionario, @ID_Publico, @ID_Usuario, @Ruc, @RazonSocial, @Tipo, @Ubigeo, @TipoDocumento, @NroDocumento, @NombreCompleto, @Correo", IDCuestionarioParameter, IDPublicoParameter, IDUsuarioParameter, RucParameter, RazonSocialParameter, TipoParameter, UbigeoParameter, TipoDocumentoParameter, NroDocumentoParameter, NombreCompletoParameter, CorreoParameter).ToList();
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
        }

        //public T_Usuario UsuarioLogin(string usuario, string contrasena)
        //{
        //    List<T_Usuario> lstUsuario;
        //    T_Usuario Resultado = new T_Usuario();

        //    SqlParameter IDUsuarioParameter = new SqlParameter("@ID_Usuario", usuario);
        //    string password = EncriptaCadena(string.Concat(usuario.ToUpper(), contrasena));
        //    SqlParameter ContrasenaParameter = new SqlParameter("@Contrasena", password);
        //    //SqlParameter ContrasenaParameter = new SqlParameter("@Contrasena", contrasena);
        //    try
        //    {
        //        lstUsuario = this.T_Usuario.SqlQuery("[dbo].[PA_Usuario_Login] @ID_Usuario, @Contrasena", IDUsuarioParameter, ContrasenaParameter).ToList();

        //        if (lstUsuario.Count > 0)
        //            Resultado = lstUsuario.ElementAt(0);
        //    }
        //    catch (SqlException ex)
        //    {
        //        SqlMsg = ex.Message;
        //    }
        //    return Resultado; /////////////////////////////////////////////
        //}

        //public T_Usuario UsuarioToken(string token)
        //{
        //    List<T_Usuario> lstUsuario;
        //    T_Usuario Resultado = new T_Usuario();

        //    SqlParameter TokenParameter = new SqlParameter("@C_Token", token);
        //    try
        //    {
        //        lstUsuario = this.T_Usuario.SqlQuery("[dbo].[PA_Usuario_Token] @C_Token", TokenParameter).ToList();

        //        if (lstUsuario.Count > 0)
        //            Resultado = lstUsuario.ElementAt(0);
        //    }
        //    catch (SqlException ex)
        //    {
        //        SqlMsg = ex.Message;
        //    }
        //    return Resultado; ///////////////////////////////////////////////
        //}

        //        public T_Usuario UsuarioTokenSI(string token)
        //        {
        //            List<T_Usuario> lstUsuario;
        //            T_Usuario Resultado = new T_Usuario();

        //            SqlParameter TokenParameter = new SqlParameter("@C_Token", token);
        //            try
        //            {
        //                lstUsuario = this.T_Usuario.SqlQuery("[dbo].[PA_Usuario_TokenSI] @C_Token", TokenParameter).ToList();

        //                if (lstUsuario.Count > 0)
        //                    Resultado = lstUsuario.ElementAt(0);
        //            }
        //            catch (SqlException ex)
        //            {
        //                SqlMsg = ex.Message;
        //            }
        //            return Resultado; ///////////////////////////////////////////////
        //        }

        //        public int RegistraUsuario(string pRuc, string pDni, string pClave, string pEmail)
        //        {
        //            SqlParameter RucParameter = new SqlParameter("@C_Ruc", pRuc);
        //            SqlParameter DniParameter = new SqlParameter("@C_Dni", pDni);
        //            SqlParameter ContrasenaParameter = new SqlParameter("@C_Contrasena", pClave);
        //            SqlParameter EmailParameter = new SqlParameter("@C_Email", pEmail);

        //            List<T_Usuario> datos;
        //            try
        //            {
        //                datos = this.T_Usuario.SqlQuery("[dbo].[PA_Usuario_Agregar] @C_Ruc, @C_Dni, @C_Contrasena, @C_Email",
        //                    RucParameter, DniParameter, ContrasenaParameter, EmailParameter).ToList(); 
        //                //datos.Count;
        //            }
        //            catch (SqlException Ex)
        //            {
        //                SqlFlag = 2;
        //                SqlMsg = Ex.Message;
        //            }
        //            return 1;
        //        }

        //public List<T_DetalleEncuesta> RegistraAlternativa(int pEncuesta, int pSeccion, int pPregunta, int pAlternativa, string pComentario, int? pIntervalo, int pUsuario)
        //{
        //    SqlParameter EncuestaParameter = new SqlParameter("@N_Encuesta", pEncuesta);
        //    SqlParameter SeccionParameter = new SqlParameter("@N_Seccion", pSeccion);
        //    SqlParameter PreguntaParameter = new SqlParameter("@N_Pregunta", pPregunta);
        //    SqlParameter AlternativaParameter = new SqlParameter("@N_Alternativa", pAlternativa);
        //    SqlParameter ComentarioParameter = new SqlParameter("@C_Comentario", pComentario);
        //    SqlParameter IntervaloParameter = new SqlParameter("@N_Intervalo", pIntervalo.Value);
        //    SqlParameter UsuarioParameter = new SqlParameter("@N_Usuario", pUsuario);

        //    List<T_DetalleEncuesta> datos = new List<T_DetalleEncuesta>();
        //    try
        //    {
        //        datos = this.T_DetalleEncuesta.SqlQuery("[dbo].[PA_DetalleEncuesta_Ins] @N_Encuesta, @N_Seccion, @N_Pregunta, @N_Alternativa, @C_Comentario, @N_Intervalo, @N_Usuario",
        //                EncuestaParameter, SeccionParameter, PreguntaParameter, AlternativaParameter, ComentarioParameter, IntervaloParameter, UsuarioParameter).ToList();
        //        //datos.Count;
        //        int valor = datos.ElementAt(0).ID_Pregunta;
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}


        public int EliminaAlternativa(int pEncuesta, int pSeccion, int pPregunta)
        {
            SqlParameter EncuestaParameter = new SqlParameter("@N_Encuesta", pEncuesta);
            SqlParameter SeccionParameter = new SqlParameter("@N_Seccion", pSeccion);
            SqlParameter PreguntaParameter = new SqlParameter("@N_Pregunta", pPregunta);

            //List<T_DetalleEncuesta> datos = new List<T_DetalleEncuesta>();
            //try
            //{
            //    int valor = 0;
            //    datos = this.T_DetalleEncuesta.SqlQuery("[dbo].[PA_AlternativaEncuesta_Elimina] @N_Encuesta, @N_Seccion, @N_Pregunta",
            //        EncuestaParameter, SeccionParameter, PreguntaParameter).ToList();
            //    if (datos.Count > 0)
            //    {
            //        valor = datos.ElementAt(0).ID_Pregunta;
            //    }

            //    return valor;
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
            return 1;
        }

        //public List<T_DetalleEncuesta> ConsultaAlternativa(int pEncuesta, int pSeccion, int pPregunta)
        //{
        //    SqlParameter EncuestaParameter = new SqlParameter("@N_Encuesta", pEncuesta);
        //    SqlParameter SeccionParameter = new SqlParameter("@N_Seccion", pSeccion);
        //    SqlParameter PreguntaParameter = new SqlParameter("@N_Pregunta", pPregunta);

        //    List<T_DetalleEncuesta> datos = new List<T_DetalleEncuesta>();
        //    try
        //    {
        //        datos = this.T_DetalleEncuesta.SqlQuery("[dbo].[PA_DetalleEncuesta_List] @N_Encuesta, @N_Seccion, @N_Pregunta",
        //                EncuestaParameter, SeccionParameter, PreguntaParameter).ToList();
        //        //datos.Count;
        //        int valor = datos.ElementAt(0).ID_Pregunta;
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_ConfiguracionOrden> RegistraConfiguracionOrden(T_ConfiguracionOrden t_ConfiguracionOrden)
        //{
        //    SqlParameter CuestionarioParameter = new SqlParameter("@N_Cuestionario", t_ConfiguracionOrden.ID_Cuestionario);
        //    SqlParameter SeccionParameter = new SqlParameter("@N_Seccion", t_ConfiguracionOrden.ID_Seccion);
        //    SqlParameter OrdenSecParameter = new SqlParameter("@N_OrdenSec", t_ConfiguracionOrden.OrdenSec);
        //    SqlParameter PreguntaParameter = new SqlParameter("@N_Pregunta", t_ConfiguracionOrden.ID_Pregunta);
        //    SqlParameter OrdenPregParameter = new SqlParameter("@N_OrdenPreg", t_ConfiguracionOrden.OrdenPreg);
        //    SqlParameter AlternativaParameter = new SqlParameter("@N_Alternativa", t_ConfiguracionOrden.ID_Alternativa);
        //    SqlParameter OrdenAlterParameter = new SqlParameter("@N_OrdenAlter", t_ConfiguracionOrden.OrdenAlter);


        //    List<T_ConfiguracionOrden> datos = new List<T_ConfiguracionOrden>();
        //    try
        //    {
        //        datos = this.T_ConfiguracionOrden.SqlQuery("[dbo].[PA_ConfiguracionOrden_Upd] @N_Cuestionario, @N_Seccion, @N_OrdenSec, @N_Pregunta, @N_OrdenPreg, @N_Alternativa, @N_OrdenAlter",
        //                CuestionarioParameter, SeccionParameter, OrdenSecParameter, PreguntaParameter, OrdenPregParameter, AlternativaParameter, OrdenAlterParameter).ToList();
        //        //datos.Count;
        //        int valor = datos.ElementAt(0).ID_Cuestionario;
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //        public int EliminaSecuencia(int pAlternativa)
        //        {
        //            SqlParameter AlternativaParameter = new SqlParameter("@N_Alternativa", pAlternativa);

        //            List<T_PreguntaSecuencia> datos;
        //            try
        //            {
        //                datos = this.T_PreguntaSecuencia.SqlQuery("[dbo].[PA_Secuencia_Elimina] @N_Alternativa",
        //                    AlternativaParameter).ToList();
        //                return 1;
        //            }
        //            catch (SqlException Ex)
        //            {
        //                SqlFlag = 2;
        //                SqlMsg = Ex.Message;
        //            }
        //            return 1;
        //        }

        //        public int AgregarSecuencia(int pCuestionario, int pAlternativa, int pPregunta, int pFinSecuencia, string pSecuencia)
        //        {
        //            SqlParameter CuestionarioParameter = new SqlParameter("@N_Cuestionario", pCuestionario);
        //            SqlParameter AlternativaParameter = new SqlParameter("@N_Alternativa", pAlternativa);
        //            SqlParameter PreguntaParameter = new SqlParameter("@N_Pregunta", pPregunta);
        //            SqlParameter FinSecuenciaParameter = new SqlParameter("@B_FinSecuencia", pFinSecuencia);
        //            SqlParameter SecuenciaParameter = new SqlParameter("@C_Secuencia", pSecuencia);

        //            List<T_PreguntaSecuencia> datos;
        //            try
        //            {
        //                datos = this.T_PreguntaSecuencia.SqlQuery("[dbo].[PA_Secuencia_Agregar] @N_Cuestionario,@N_Alternativa,@N_Pregunta,@B_FinSecuencia,@C_Secuencia",
        //                    CuestionarioParameter, AlternativaParameter, PreguntaParameter, FinSecuenciaParameter, SecuenciaParameter).ToList();
        //                return 1;
        //            }
        //            catch (SqlException Ex)
        //            {
        //                SqlFlag = 2;
        //                SqlMsg = Ex.Message;
        //            }
        //            return 1;
        //        }

        public int AgregarInvitacionVP(int pCuestionario, int pUsuario)
        {
            SqlParameter CuestionarioParameter = new SqlParameter("@N_Cuestionario", pCuestionario);
            SqlParameter UsuarioParameter = new SqlParameter("@N_Usuario", pUsuario);

            //List<T_Encuesta> datos;

            int ID = 0;
            //try
            //{
            //    datos = this.T_Encuesta.SqlQuery("[dbo].[PA_InvitacionVP_Agregar] @N_Cuestionario, @N_Usuario", CuestionarioParameter, UsuarioParameter).ToList();
            //    if (datos.Count() > 0)
            //    {
            //        ID = datos.ElementAt(0).ID_Encuesta;
            //    }
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
            return ID;
        }

        //public List<NotificacionDto> ListarEncuestasPendientes(string IdUsuario, int NRol, string CRuc)
        //{
        //    SqlParameter IdUsuarioParameter = new SqlParameter("@IdUsuario", IdUsuario);
        //    SqlParameter RolParameter = new SqlParameter("@NRol", NRol);
        //    SqlParameter RucParameter = new SqlParameter("@CRuc", CRuc);


        //    List<NotificacionDto> datos = new List<NotificacionDto>();
        //    try
        //    {
        //        datos = this.Database.SqlQuery<NotificacionDto>("[dbo].[PA_ListarEncuestasPendientes] @IdUsuario, @NRol, @CRuc",
        //                IdUsuarioParameter, RolParameter, RucParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_AvanceEncuestas> ListarAvanceEncuestas(int IdCuestionario)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);

        //    List<T_AvanceEncuestas> datos = new List<T_AvanceEncuestas>();
        //    try
        //    {
        //        datos = this.T_AvanceEncuestasEntidad.SqlQuery("[dbo].[PA_AvanceEncuesta] @IDCuestionario", IdCuestionarioParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_AvanceEncuestasDetalle> ListarAvanceEncuestasDetalle(string CRuc, int IdCuestionario)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);
        //    SqlParameter RucParameter = new SqlParameter("@CRuc", CRuc);

        //    List<T_AvanceEncuestasDetalle> datos = new List<T_AvanceEncuestasDetalle>();
        //    try
        //    {
        //        datos = this.T_AvanceEncuestasEntidadDetalle.SqlQuery("[dbo].[PA_AvanceEncuestaDetalle] @IDCuestionario, @CRuc", IdCuestionarioParameter, RucParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_AvanceEncuestasProveedor> ListarAvanceEncuestasProveedor(int IdCuestionario)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);

        //    List<T_AvanceEncuestasProveedor> datos = new List<T_AvanceEncuestasProveedor>();
        //    try
        //    {
        //        datos = this.T_AvancceEncuestasProveedor.SqlQuery("[dbo].[PA_AvanceEncuestasProveedor] @IDCuestionario", IdCuestionarioParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_AvanceLlenadoEncuestas> ListarLlenadoEncuesta(int IdCuestionario, int pEstadoE)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);
        //    SqlParameter IdEstadoEParameter = new SqlParameter("@IDEstadoE", pEstadoE);

        //    List<T_AvanceLlenadoEncuestas> datos = new List<T_AvanceLlenadoEncuestas>();
        //    try
        //    {
        //        datos = this.T_AvanceLlenadoEncuestas.SqlQuery("[dbo].[PA_AvanceLlenadoEncuestas] @IDCuestionario,@IDEstadoE", IdCuestionarioParameter, IdEstadoEParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_AvanceLlenadoEncuestasComentario> ListarLlenadoEncuestaComentario(int IdCuestionario, int IdSeccion, int IdPregunta, int IdAlternativa, int IdEscala, int EstadoE)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);
        //    SqlParameter IdSeccionParameter = new SqlParameter("@IDSeccion", IdSeccion);
        //    SqlParameter IdPreguntaParameter = new SqlParameter("@IDPregunta", IdPregunta);
        //    SqlParameter IdAlternativaParameter = new SqlParameter("@IDAlternativa", IdAlternativa);
        //    SqlParameter IdEscalaParameter = new SqlParameter("@IDEscala", IdEscala);
        //    SqlParameter EstadoEParameter = new SqlParameter("@IDEstadoE", EstadoE);

        //    List<T_AvanceLlenadoEncuestasComentario> datos = new List<T_AvanceLlenadoEncuestasComentario>();
        //    try
        //    {
        //        datos = this.T_AvanceLlenadoEncuestasComentarios.SqlQuery("[dbo].[PA_AvanceLlenadoEncuestasComentario] @IDCuestionario, @IDSeccion, @IDPregunta, @IDAlternativa, @IDEscala, @IDEstadoE",
        //            IdCuestionarioParameter, IdSeccionParameter, IdPreguntaParameter, IdAlternativaParameter, IdEscalaParameter, EstadoEParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        public List<T_Cuestionario> ListaCuestionariosUsuario(string Usuario)
        {
            SqlParameter UsuarioParameter = new SqlParameter("@Usuario", Usuario);
            List<T_Cuestionario> lst = new List<T_Cuestionario>();
            try
            {
                lst = this.T_Cuestionario.SqlQuery("[dbo].[PA_Cuestionario_List] @Usuario", UsuarioParameter).ToList();
                return lst;
            }
            catch (SqlException Ex)
            {
                SqlFlag = 2;
                SqlMsg = Ex.Message;
            }
            return lst;
        }

        //public List<T_Seccion> ListaSeccionesUsuario(string Usuario)
        //{
        //    SqlParameter UsuarioParameter = new SqlParameter("@Usuario", Usuario);
        //    List<T_Seccion> lst = new List<T_Seccion>();
        //    try
        //    {
        //        lst = this.T_Seccion.SqlQuery("[dbo].[PA_seccion_List] @Usuario", UsuarioParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_Pregunta> ListaPreguntasUsuario(string Usuario)
        //{
        //    SqlParameter UsuarioParameter = new SqlParameter("@Usuario", Usuario);
        //    List<T_Pregunta> lst = new List<T_Pregunta>();
        //    try
        //    {
        //        lst = this.T_Pregunta.SqlQuery("[dbo].[PA_Pregunta_List] @Usuario", UsuarioParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_CboEstado> ListarCboEstado(string CTabla, int IDEstadoPadre)
        //{
        //    SqlParameter UsuarioParameter = new SqlParameter("@CTabla", CTabla);
        //    SqlParameter IDEstadoPadreParameter = new SqlParameter("@IDEstadoPadre", IDEstadoPadre);

        //    List<T_CboEstado> lst = new List<T_CboEstado>();
        //    try
        //    {
        //        lst = this.T_CboEstado.SqlQuery("[dbo].[PA_ListEstado] @CTabla,@IDEstadoPadre", UsuarioParameter, IDEstadoPadreParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_EstadoInvitacionDatos> ListarEstadoInvitacionDatos(int IDCuestionario, int IDEstado)
        //{
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IDCuestionario", IDCuestionario);
        //    SqlParameter IDEstadoParameter = new SqlParameter("@IDEstado", IDEstado);

        //    List<T_EstadoInvitacionDatos> lst = new List<T_EstadoInvitacionDatos>();
        //    try
        //    {
        //        lst = this.T_EstadoInvitacionDatos.SqlQuery("[dbo].[PA_RptEstadoInvitacion] @IDCuestionario,@IDEstado", IDCuestionarioParameter, IDEstadoParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_AccesoInvitacion> ListarAccesoInvitacionesNoEnviadas(int IDUsuario, int IDCuestionario, int IDCorreo, int N_Registro)
        //{
        //    SqlParameter IDUsuarioParameter = new SqlParameter("@IdUsuario", IDUsuario);
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IdCuestionario", IDCuestionario);
        //    SqlParameter IDCorreoParameter = new SqlParameter("@IdCorreo", IDCorreo);
        //    SqlParameter RegistroParameter = new SqlParameter("@N_Registro", N_Registro);

        //    List<T_AccesoInvitacion> lst = new List<T_AccesoInvitacion>();
        //    try
        //    {
        //        lst = this.T_AccesoInvitacion.SqlQuery("[dbo].[PA_ListInvitacionesNoEnviadas] @IdUsuario,@IDCuestionario,@IdCorreo,@N_Registro", IDUsuarioParameter, IDCuestionarioParameter, IDCorreoParameter, RegistroParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_AccesoInvitacion> ListarAccesoInvitacionesEnviadas(int IDUsuario, int IDCuestionario, int IDCorreo, int N_Registro, string CTipo, string CUbigeo, int IdEstadoE)
        //{
        //    SqlParameter IDUsuarioParameter = new SqlParameter("@IdUsuario", IDUsuario);
        //    SqlParameter IDCuestionarioParameter = new SqlParameter("@IdCuestionario", IDCuestionario);
        //    SqlParameter IDCorreoParameter = new SqlParameter("@IdCorreo", IDCorreo);
        //    SqlParameter RegistroParameter = new SqlParameter("@N_Registro", N_Registro);
        //    SqlParameter IDEstadoEParameter = new SqlParameter("@ID_EstadoE", IdEstadoE);
        //    SqlParameter CTipoParameter = new SqlParameter("@CTipo", CTipo);
        //    SqlParameter CUbigeoParameter = new SqlParameter("@CUbigeo", CUbigeo);

        //    List<T_AccesoInvitacion> lst = new List<T_AccesoInvitacion>();
        //    try
        //    {
        //        lst = this.T_AccesoInvitacion.SqlQuery("[dbo].[PA_ListInvitacionesEnviadas] @IdUsuario,@IDCuestionario,@IdCorreo,@N_Registro,@ID_EstadoE,@CTipo,@CUbigeo", IDUsuarioParameter, IDCuestionarioParameter, IDCorreoParameter, RegistroParameter, IDEstadoEParameter, CTipoParameter, CUbigeoParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        public void ActualizarEstadoInvitacion(int idInvitacion, string Usuario, int idEstado, int proc)
        {
            SqlParameter IDInvitacionParameter = new SqlParameter("@ID_Invitacion", idInvitacion);
            SqlParameter UsuarioParameter = new SqlParameter("@Usuario", Usuario);
            SqlParameter IDEstadoParameter = new SqlParameter("@ID_Estado", idEstado);
            SqlParameter EnviarParameter = new SqlParameter("@Enviar", proc);

            //List<T_Invitacion> lst;
            //try
            //{
            //    lst = this.T_Invitacion.SqlQuery("[dbo].[PA_InvitacionEstado_Upd] @ID_Invitacion,@Usuario,@ID_Estado,@Enviar", IDInvitacionParameter, UsuarioParameter, IDEstadoParameter, EnviarParameter).ToList();
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
        }

        //public List<T_AvanceMuestreo> ListarAvanceMuestreo(int IdCuestionario)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);

        //    List<T_AvanceMuestreo> datos = new List<T_AvanceMuestreo>();
        //    try
        //    {
        //        datos = this.T_AvanceMuestreo.SqlQuery("[dbo].[PA_AvanceMuestreo] @IDCuestionario", IdCuestionarioParameter).ToList();
        //        return datos;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return datos;
        //}

        //public List<T_CboTipoMuestreo> ListarCboTipoMuestreo(int? IdCuestionario)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);

        //    List<T_CboTipoMuestreo> lst = new List<T_CboTipoMuestreo>();
        //    try
        //    {
        //        lst = this.T_CboTipoMuestreo.SqlQuery("[dbo].[PA_ListTipoMuestreo] @IDCuestionario", IdCuestionarioParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        //public List<T_Muestreo> ListMuestreo (int? IdCuestionario, string IdTipoMuestreo)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);
        //    SqlParameter IdTipoMuestreoParameter = new SqlParameter("@IDTipoMuestreo", IdTipoMuestreo);

        //    List<T_Muestreo> lst = new List<T_Muestreo>();
        //    try
        //    {
        //        lst = this.T_Muestreo.SqlQuery("[dbo].[PA_ListMuestreo] @IDCuestionario, @IDTipoMuestreo", IdCuestionarioParameter, IdTipoMuestreoParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        public void GrabarCantMuestra(int idMuestra, int cantidad)
        {
            SqlParameter IDMuestreoParameter = new SqlParameter("@IDMuestreo", idMuestra);
            SqlParameter NMuestraParameter = new SqlParameter("@NMuestra", cantidad);

            //List<T_Muestreo> lst;
            //try
            //{
            //    lst = this.T_Muestreo.SqlQuery("[dbo].[PA_Muestreo_Upd] @IDMuestreo,@NMuestra", IDMuestreoParameter, NMuestraParameter).ToList();
            //}
            //catch (SqlException Ex)
            //{
            //    SqlFlag = 2;
            //    SqlMsg = Ex.Message;
            //}
        }

        //public List<T_CboEstrato> ListCboMuestreo(int? IdCuestionario, string IdTipoMuestreo)
        //{
        //    SqlParameter IdCuestionarioParameter = new SqlParameter("@IDCuestionario", IdCuestionario);
        //    SqlParameter IdTipoMuestreoParameter = new SqlParameter("@IDTipoMuestreo", IdTipoMuestreo);

        //    List<T_CboEstrato> lst = new List<T_CboEstrato>();
        //    try
        //    {
        //        lst = this.T_CboEstrato.SqlQuery("[dbo].[PA_ListCboEstrato] @IDCuestionario, @IDTipoMuestreo", IdCuestionarioParameter, IdTipoMuestreoParameter).ToList();
        //        return lst;
        //    }
        //    catch (SqlException Ex)
        //    {
        //        SqlFlag = 2;
        //        SqlMsg = Ex.Message;
        //    }
        //    return lst;
        //}

        public List<T_Cuestionario> ListCuestionario(int ID_Invitacion)
        {
            SqlParameter IdInvitacionParameter = new SqlParameter("@IDInvitacion", ID_Invitacion);

            List<T_Cuestionario> lst = new List<T_Cuestionario>();
            try
            {
                lst = this.T_Cuestionario.SqlQuery("[dbo].[PA_ListCuestionarioInvitacion] @IDInvitacion", IdInvitacionParameter).ToList();
                return lst;
            }
            catch (SqlException Ex)
            {
                SqlFlag = 2;
                SqlMsg = Ex.Message;
            }
            return lst;
        }

    }
}