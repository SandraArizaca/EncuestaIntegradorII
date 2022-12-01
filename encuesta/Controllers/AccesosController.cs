using System;

using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using encuesta.Contexto;
using encuesta.Models;
using encuesta.wsAutenticaEntidad;

//namespace SISCatalogo.Controllers
namespace encuesta.Controllers
{
    public class AccesosController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        /*public ActionResult Index()
        {

            if (TempData.ContainsKey("Rol") && TempData["Rol"] != null && Convert.ToInt32(TempData.Peek("Rol")) > 0)
            {
                return RedirectToAction("Index", "Encuesta");
            }
            else
            {
                string msg = string.Empty;

                if (TempData.ContainsKey("Msg") && TempData["Msg"] != null && TempData["Msg"].ToString().Length > 0)
                {
                    msg = TempData["Msg"].ToString();
                }

                TempData.Clear();
                Session.Abandon();

                if (msg.Length > 0)
                {
                    TempData["Msg"] = msg;
                }

                return View();
            }
        }*/

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Token()
        //{
        //    string CToken = "";
        //    if (TempData.ContainsKey("Token"))
        //    {
        //        CToken = TempData.Peek("Token").ToString();
        //    }

        //    T_Usuario Resultado = new T_Usuario();
        //    Resultado = DB.UsuarioToken(CToken);

        //    if (Resultado.ID_Usuario != 0)
        //    {
        //        TempData["IDUsuario"] = Resultado.ID_Usuario;
        //        TempData.Keep("IDUsuario");
        //        return RedirectToAction("Index", "Encuesta");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult token(string token)
        {

            ViewBag.C_Usuario = "";

            TempData.Clear();

            if (token.Length > 0)
            {
                T_Usuario Resultado = new T_Usuario();

                //if (tipo == 1)
                //{
                    Resultado = DB.UsuarioToken(token);
                //}
                //else
                //{
                //    Resultado = DB.UsuarioTokenSI(token);
                //}
                if (Resultado.ID_Usuario != 0)
                {
                    //Recuperar Mensajes si usuario es valido
                    if (Resultado.ID_Usuario == 0)
                    {
                        TempData["Msg"] = "El usuario o la contraseña son incorrectos .!";
                        TempData["Rol"] = 0;

                        return View();
                    }
                    else //Usuario y contraseña validos ------------------------------
                    {
                        TempData["Token_Encuesta_Publico"] = token;
                        TempData["Rol"] = Resultado.ID_Publico;
                        TempData["IDUsuario"] = Resultado.ID_Usuario;
                        TempData["Usuario"] = Resultado.C_Usuario;
                        TempData["RazonSocial"] = "";

                        TempData.Keep("Token_Encuesta_Publico");
                        TempData.Keep("Rol");
                        TempData.Keep("IDUsuario");
                        TempData.Keep("Usuario");
                        TempData.Keep("RazonSocial");

                        return RedirectToAction("Index", "Encuesta");
                    }

                }
                else
                {
                    TempData["Msg"] = "Error en la validación del usuario";
                    TempData["Rol"] = 0;
                    return RedirectToAction("Index", "Encuesta");
                }
            }

            return RedirectToAction("Index", "Encuesta");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////public ActionResult Index(string ID_Usuario, string Contrasena, string optAcceso)
        //public ActionResult Index(string ID_Usuario, 
        //                          string Contrasena, 
        //                          string optAcceso, 
        //                          string idEntidad, 
        //                          string razonSocial, 
        //                          string nombres,
        //                          string apellidos,
        //                          string tipoDocumento, 
        //                          string numeroDocumento, 
        //                          string email)
        //{
        //    TempData.Clear();

        //    if (ID_Usuario.Length > 0 && Contrasena.Length > 0)
        //    {
        //        T_Usuario_Ant Resultado = new T_Usuario_Ant();

        //        Resultado = DB.UsuarioLogin(ID_Usuario, Contrasena);
        //        if (Resultado.N_Rol != 0)
        //        {
        //            //Recuperar Mensajes si usuario es valido
        //            if (Resultado.N_Rol == 0)
        //            {
        //                TempData["Msg"] = "El usuario o la contraseña son incorrectos .!";
        //                TempData["Rol"] = 0;

        //                return View();
        //            }
        //            else //Usuario y contraseña validos ------------------------------
        //            {
        //                TempData["Token"] = Resultado.C_Token_Acceso;
        //                TempData["Rol"] = Resultado.N_Rol;
        //                TempData["Usuario"] = Resultado.ID_Usuario;
        //                TempData["RazonSocial"] = "";

        //                TempData.Keep("Token");
        //                TempData.Keep("Rol");
        //                TempData.Keep("Usuario");
        //                TempData.Keep("RazonSocial");

        //                return RedirectToAction("Index", "Encuesta");
        //            }

        //        }
        //        else
        //        {
        //            TempData["Msg"] = "El usuario o la contraseña son incorrectos .!";
        //            TempData["Rol"] = 0;
        //            return View();
        //        }
        //    }

        //    return RedirectToAction("Index", "Encuesta");
        //}

        //[HttpGet]
        //public ActionResult token(string token, int tipo)
        //{

        //    ViewBag.C_Usuario = "";

        //    TempData.Clear();

        //    if (token.Length > 0 )
        //    {
        //        T_Usuario_Ant Resultado = new T_Usuario_Ant();

        //        if (tipo == 1)
        //        {
        //            Resultado = DB.UsuarioToken(token);
        //        }else
        //        {
        //            Resultado = DB.UsuarioTokenSI(token);
        //        }
        //        if (Resultado.N_Rol != 0)
        //        {
        //            //Recuperar Mensajes si usuario es valido
        //            if (Resultado.N_Rol == 0)
        //            {
        //                TempData["Msg"] = "El usuario o la contraseña son incorrectos .!";
        //                TempData["Rol"] = 0;

        //                return View();
        //            }
        //            else //Usuario y contraseña validos ------------------------------
        //            {
        //                TempData["Token"] = Resultado.C_Token_Acceso;
        //                TempData["Rol"] = Resultado.N_Rol;
        //                TempData["Usuario"] = Resultado.ID_Usuario;
        //                TempData["RazonSocial"] = "";

        //                TempData.Keep("Token");
        //                TempData.Keep("Rol");
        //                TempData.Keep("Usuario");
        //                TempData.Keep("RazonSocial");

        //                return RedirectToAction("Index", "Encuesta");
        //            }

        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Error en la validación del usuario";
        //            TempData["Rol"] = 0;
        //            return RedirectToAction("Index", "Encuesta");
        //        }
        //    }

        //    return RedirectToAction("Index", "Encuesta");
        //    //return View();
        //}

        ////al seleccionar el radio entidad, se lista las entidades asociadas al usuario
        //public ActionResult _DatosEntidad(string ID_Usuario, string Contrasena)
        //{
        //    System.Configuration.Configuration webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/demouser");
        //    System.Configuration.KeyValueConfigurationElement TipoAmbienteSetting = webConfig.AppSettings.Settings["TipoAmbiente"];

        //    TempData.Clear();
        //    List<m_EntidadWS> lstEnditades = new List<m_EntidadWS>();

        //    if (ID_Usuario != null && Contrasena != null)
        //    {
        //        if (ID_Usuario.Length > 0 && Contrasena.Length > 0)
        //        {
        //            try
        //            {
        //                encuesta.wsAutenticaEntidad.ValidarCredencialIn objValidarCredencialIn = new encuesta.wsAutenticaEntidad.ValidarCredencialIn();
        //                encuesta.wsAutenticaEntidad.ValidarCredencialOut objValidarCredencialOut = new encuesta.wsAutenticaEntidad.ValidarCredencialOut();
        //                encuesta.wsAutenticaEntidad.AutenticadorSeacePortTypeClient objAcutenticaEntidad = new encuesta.wsAutenticaEntidad.AutenticadorSeacePortTypeClient();
        //                encuesta.wsAutenticaEntidad.CredencialT01 objCredencial = new encuesta.wsAutenticaEntidad.CredencialT01();

        //                objCredencial.username = ID_Usuario;
        //                objCredencial.password = Contrasena;
        //                objValidarCredencialIn.credencialT01 = objCredencial;

        //                objAcutenticaEntidad.Open();
        //                objValidarCredencialOut = objAcutenticaEntidad.validarCredencial(objValidarCredencialIn);
        //                objAcutenticaEntidad.Close();

        //                if (objValidarCredencialOut.resultado.codigo.Equals("00"))
        //                {
        //                    if (objValidarCredencialOut.usuario.estado.Equals("ACTIVO"))
        //                    {
        //                        if (objValidarCredencialOut.usuario.organismos != null && objValidarCredencialOut.usuario.organismos.Length > 0)
        //                        {
        //                            ViewBag.nombres = objValidarCredencialOut.usuario.nombres;
        //                            ViewBag.apellidos = objValidarCredencialOut.usuario.apellidos;
        //                            ViewBag.nombreCompleto = objValidarCredencialOut.usuario.nombreCompleto;
        //                            ViewBag.tipoDocumento = objValidarCredencialOut.usuario.tipoDocumento;
        //                            ViewBag.numeroDocumento = objValidarCredencialOut.usuario.numeroDocumento;
        //                            ViewBag.email = objValidarCredencialOut.usuario.email;

        //                            for (int i = 0; i < objValidarCredencialOut.usuario.organismos.Length; i++)
        //                            {
        //                                m_EntidadWS objEntidad = new m_EntidadWS();
        //                                objEntidad.IdEntidad = objValidarCredencialOut.usuario.organismos.ElementAt(i).idOrganismo + "-" + objValidarCredencialOut.usuario.organismos.ElementAt(i).numeroDocumento;
        //                                objEntidad.razonSocial = objValidarCredencialOut.usuario.organismos.ElementAt(i).razonSocial;
        //                                lstEnditades.Add(objEntidad);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            TempData["Msg"] = "No se encontraron entidades para el usuario";
        //                            TempData["Rol"] = 0;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TempData["Msg"] = "El usuario se encuentra en estado: " + objValidarCredencialOut.usuario.estado;
        //                        TempData["Rol"] = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    if (objValidarCredencialOut.resultado.codigo != null)
        //                    {
        //                        TempData["Msg"] = objValidarCredencialOut.resultado.mensaje;
        //                        TempData["Rol"] = 0;
        //                    }
        //                    else
        //                    {
        //                        TempData["Msg"] = "No se encontraron datos de usuario";
        //                        TempData["Rol"] = 0;
        //                    }
        //                }

        //            }
        //            catch
        //            {
        //                TempData["Msg"] = "Ha ocurrido un error de conexión al realizar la autenticación con OSCE, por favor inténtelo más tarde";
        //                TempData["Rol"] = 0;
        //            }
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Ingrese datos";
        //            TempData["Rol"] = 0;
        //        }
        //    }

        //    return View(lstEnditades);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    TempData.Clear();

        //    //Session.Abandon();
        //    //Session.RemoveAll();

        //    //FormsAuthentication.SignOut();

        //    this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //    this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    this.Response.Cache.SetNoStore();

        //    TempData["Msg"] = "Salio del sistema!!";
        //    TempData["Rol"] = 0;

        //    //return RedirectToAction(actionName: "Index",
        //    //                        controllerName: "Accesos",
        //    //                        routeValues: new { controller = "Accesos", action = "Index" });

        //    return RedirectToAction("Index", "Accesos");

        //}

        //public ActionResult LoginCorreo()
        //{
        //    TempData.Clear();
        //    //Response.Cookies["AuthID"].Expires = DateTime.Now.AddDays(-1);
        //    //Session.Abandon();

        //    return View();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
