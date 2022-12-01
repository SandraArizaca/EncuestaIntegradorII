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
    [TokenFilter]
    public class ReporteController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();
        private string cVista;

        public ActionResult InvitacionRechazada(int? N_Cuestionario)
        {
            if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            List<T_CboEstado> lstEstado = new List<T_CboEstado>();
            if (cUsuario == "MultiencuestaDAMER")
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true && item.ID_Area <= 3).ToList();
            }
            else
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            }

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            lstEstado = DB.ListarCboEstado("T_Invitacion", 0);

            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;
            ViewBag.estado = lstEstado;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();

            if (lstCuestionario.Count == 0)
            {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }

            ViewBag.N_Cuestionario = N_Cuestionario.Value;

            return View();
        }
        
        public ActionResult EstadoInvitacion_Datos(int? pCuestionario, int pEstado)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }

            List<T_EstadoInvitacionDatos> lst = new List<T_EstadoInvitacionDatos>();
            lst = DB.ListarEstadoInvitacionDatos(pCuestionario.Value, pEstado);

            return PartialView(lst);
        }
    }
}
