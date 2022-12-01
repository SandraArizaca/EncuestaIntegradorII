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
    public class SeguimientoController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();
        private string cVista;

        public ActionResult AvanceEncuesta(int? N_Cuestionario)
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
            if (cUsuario == "MultiencuestaDAMER")
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true && item.ID_Area <= 3).ToList();
            }
            else
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            }

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

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

        public ActionResult AvanceEncuesta_Datos(int? pCuestionario)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario.Value).ToList();
            
            int N_Publico = 0;

          
            if (lstCuestionario.Count > 0)
            {
                N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }
            else
            {
                N_Publico = 100;
            }

            cVista = "EncuestasEntidadProveedor";
            List<T_AvanceEncuestas> lst = new List<T_AvanceEncuestas>();
            lst = DB.ListarAvanceEncuestas(pCuestionario.Value);
            ViewBag.N_Publico = N_Publico;
            return PartialView(cVista, lst);
        }

        public ActionResult EncuestasEntidadProveedor_Detalle(string C_Ruc, string C_RazonSocial, int? ID_Cuestionario)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }

            List<T_AvanceEncuestasDetalle> lst = new List<T_AvanceEncuestasDetalle>();
            lst = DB.ListarAvanceEncuestasDetalle(C_Ruc, ID_Cuestionario.Value);

            ViewBag.C_Entidad = C_Ruc + " - " + C_RazonSocial;

            int N_Publico = 0;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value).ToList();
            if (lstCuestionario.Count > 0)
            {
                N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }
            else
            {
                N_Publico = 100;
            }

            ViewBag.N_Publico = N_Publico;

            return PartialView(lst);
        }

        public ActionResult AvanceLlenado(int? N_Cuestionario)
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
            if (cUsuario == "MultiencuestaDAMER")
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true && item.ID_Area <= 3).ToList();
            }
            else
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            }

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

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

        public ActionResult Llenado_datos(int? pCuestionario, int pEstadoE)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            List<T_AvanceLlenadoEncuestas> lst = new List<T_AvanceLlenadoEncuestas>();
            lst = DB.ListarLlenadoEncuesta(pCuestionario.Value, pEstadoE);
            return PartialView(lst);
        }

        public ActionResult LLenadoDatos_Comentarios(int? ID_Cuestionario, int ID_Seccion, int ID_Pregunta, int ID_Alternativa, int ID_Escala, int EstadoE)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            List<T_AvanceLlenadoEncuestasComentario> lst = new List<T_AvanceLlenadoEncuestasComentario>();
            lst = DB.ListarLlenadoEncuestaComentario(ID_Cuestionario.Value, ID_Seccion, ID_Pregunta, ID_Alternativa, ID_Escala, EstadoE);

            ViewBag.C_Seccion = lst.ElementAt(0).C_Seccion;
            ViewBag.C_Pregunta = lst.ElementAt(0).C_Pregunta;

            if (lst.ElementAt(0).C_Escala == null)
            {
                ViewBag.C_Alternativa = lst.ElementAt(0).C_Alternativa;
            }
            else
            {
                ViewBag.C_Alternativa = lst.ElementAt(0).C_Alternativa + " - " + lst.ElementAt(0).C_Escala;
            }

            return PartialView(lst);
        }

        public ActionResult AvanceMuestreo(int? N_Cuestionario)
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

            if (cUsuario == "MultiencuestaDAMER")
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true && item.ID_Area <= 3).ToList();
            }
            else
            {
                lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            }

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

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

        public ActionResult Muestreo_datos(int? pCuestionario)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            List<T_AvanceMuestreo> lst = new List<T_AvanceMuestreo>();
            lst = DB.ListarAvanceMuestreo(pCuestionario.Value);

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario).ToList();

            if (lstCuestionario.Count > 0)
            {
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Publico = 0;
            }
            return PartialView(lst);
        }
    }
}
