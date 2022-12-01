using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using encuesta.Contexto;
using encuesta.FIlters;
using encuesta.Models;

namespace encuesta.Controllers
{
    public class LoginController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();
        // GET: Login
        public ActionResult Index()
        {
            if (TempData.ContainsKey("Token") && TempData["Token"] != null)
            {
                //return RedirectToAction("Index", "Login");
                return RedirectToAction("Registro", "Cuestionario");
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string ID_Usuario,
                                  string Contrasena,
                                  string apellidos,
                                  string nombres)
        {
            TempData.Clear();

            if (ID_Usuario.Length > 0 && Contrasena.Length > 0)
            {
                T_Usuario Resultado = new T_Usuario();

                Resultado = DB.ValidaLogin(ID_Usuario, Contrasena);
                if (Resultado.ID_Usuario != 0)
                {
                    if (Resultado.N_Estado == 2)
                    {
                        TempData["Msg"] = "El usuario esta bloqueado.!";
                        TempData["Rol"] = 0;
                        return View();
                    }
                    else
                    {
                        TempData["Perfil"] = Resultado.T_Perfil.C_Perfil;
                        TempData["IDUsuario"] = Resultado.ID_Usuario;
                        TempData["Usuario"] = Resultado.C_Usuario;
                        TempData["C_Usuario"] = Resultado.C_NombreCompleto;
                        TempData["Token"] = Resultado.C_Token_Acceso;

                        TempData.Keep("Rol");
                        TempData.Keep("Usuario");
                        TempData.Keep("C_Usuario");
                        TempData.Keep("Token");
                    }

                    return RedirectToAction("Registro", "Cuestionario");
                }
                else
                {
                    TempData["Msg"] = "El usuario o la contraseña son incorrectos.!";
                    TempData["Rol"] = 0;
                    return View();
                }
            }
            return RedirectToAction("Index", "Login");
        }
        [TokenFilter]
        public ActionResult cerrarSesion()
        {
            string strToken = "";
            try
            {
                strToken = TempData.Peek("Token").ToString();
                string[] respuesta = DB.cerrarSesion(strToken);
            }catch(Exception e)
            {

            }
            TempData.Clear();
            TempData["Msg"] = "Su sesión ha finalizado correctamente!";
            return RedirectToAction("Index", "Login");
        }
    }
}