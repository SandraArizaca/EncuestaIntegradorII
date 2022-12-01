using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using encuesta.Contexto;
using encuesta.Models;
using System.Data.SqlClient;
using encuesta.FIlters;

namespace encuesta.Controllers
{
    [TokenFilter]
    public class CrudController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult PreguntaClasifica()
        //{
        //    List<TB_Clasificacion> lstClasificacion = new List<TB_Clasificacion>();
        //    string strSql = "SELECT ID_Clasificacion, C_Clasificacion, N_Estado FROM T_Clasificacion ORDER BY C_Clasificacion";
        //    lstClasificacion = DB.TB_Clasificacion.SqlQuery(strSql).ToList();
        //    return View(lstClasificacion);
        //}


        //Clasificación de Preguntas
        public ActionResult Clasificacion(int? ID_Clasificacion)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }
            List<T_ClasificacionPregunta> lst = new List<T_ClasificacionPregunta>();
            lst = DB.T_ClasificacionPregunta.SqlQuery("PA_ClasificacionPregunta_List '" + Usuario + "'").ToList();
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            return View(lst);

            //// List<T_Clasificacion> lst = new List<T_Clasificacion>();
            //List<T_ClasificacionPregunta> lst = new List<T_ClasificacionPregunta>();
            ////string strSql = "SELECT ID_Clasificacion, C_Clasificacion, N_Estado FROM T_Clasificacion ORDER BY C_Clasificacion";
            ////lst = DB.T_Clasificacion.SqlQuery(strSql).ToList();
            //lst = DB.T_ClasificacionPregunta.SqlQuery("PA_ClasificacionPregunta_List2").ToList();
            //return View(lst);
        }

        [HttpPost]
        public ActionResult Clasificacion(int? ID_Clasificacion, string C_Clasificacion, bool? N_Estado, int isEdit, int isDelete)
        {
            //var tabla = new T_Clasificacion();
            var tabla = new T_ClasificacionPregunta();
            if (isEdit == 1 || isDelete == 1)
            {
                //tabla = new T_Clasificacion { ID_Clasificacion = ID_Clasificacion.Value };
                tabla = new T_ClasificacionPregunta { ID_ClasificacionPregunta = ID_Clasificacion.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                //DB.T_Clasificacion.Attach(tabla);
                DB.T_ClasificacionPregunta.Attach(tabla);
                if (isDelete == 1)
                {
                    DB.T_ClasificacionPregunta.Remove(tabla);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_ClasificacionPregunta.Add(tabla);
                        tabla.N_Estado = true;
                        tabla.A_CreacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        //if (!N_Estado.HasValue) { N_Estado = true; }
                        //tabla.N_Estado = N_Estado.Value;
                        tabla.A_ModificacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_ModificacionFecha = DateTime.Now;
                    }
                    tabla.C_ClasificacionPregunta = C_Clasificacion;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception)
                {

                }
                return RedirectToAction("Clasificacion", "Crud");
            }
        }

        public ActionResult Clasificacion_edit(int? ID_Clasificacion)
        {
            if (!ID_Clasificacion.HasValue) { ID_Clasificacion = 0; }
            //List<T_Clasificacion> lst = new List<T_Clasificacion>();
            List<T_ClasificacionPregunta> lst = new List<T_ClasificacionPregunta>();
            SqlParameter IDClasificacionParameter = new SqlParameter("@IDClasificacion", ID_Clasificacion.Value);
            //string strSql = "SELECT ID_Clasificacion,C_Clasificacion,N_Estado"
            //        + " FROM T_Clasificacion AS t"
            //        + " WHERE t.ID_Clasificacion = " + ID_Clasificacion.Value;
            //lst = DB.T_Clasificacion.SqlQuery(strSql).ToList();
            lst = DB.T_ClasificacionPregunta.SqlQuery("[dbo].[PA_ExisteClasificacionPregunta] @IDClasificacion", IDClasificacionParameter).ToList();
            ViewBag.ID_Clasificacion = lst.ElementAt(0).ID_ClasificacionPregunta;
            ViewBag.C_Clasificacion = lst.ElementAt(0).C_ClasificacionPregunta;
            ViewBag.N_Estado = lst.ElementAt(0).N_Estado;
            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult Clasificacion_delete(int? ID_Clasificacion)
        {
            if (!ID_Clasificacion.HasValue) { ID_Clasificacion = 0; }
            ViewBag.ID_Clasificacion = ID_Clasificacion;
            return PartialView();
        }


        //Público objetivo
        public ActionResult Publico(int? ID_Publico)
        {
            List<T_Publico> lst = new List<T_Publico>();
            //string strSql = "SELECT ID_Publico, C_Publico, N_Estado FROM T_Publico ORDER BY C_Publico";
            lst = DB.T_Publico.SqlQuery("PA_Publico_List").ToList();
            return View(lst);
        }

        [HttpPost]
        public ActionResult Publico(int? ID_Publico, string C_Publico, bool? N_Estado, int isEdit, int isDelete)
        {
            var tabla = new T_Publico();
            if (isEdit == 1 || isDelete == 1)
            {
                tabla = new T_Publico { ID_Publico = ID_Publico.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Publico.Attach(tabla);
                if (isDelete == 1)
                {
                    DB.T_Publico.Remove(tabla);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_Publico.Add(tabla);
                        tabla.N_Estado = true;
                        tabla.A_CreacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        if (!N_Estado.HasValue) { N_Estado = true; }
                        tabla.N_Estado = N_Estado.Value;
                        tabla.A_ModificacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_ModificacionFecha = DateTime.Now;
                    }
                    tabla.C_Publico = C_Publico;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception)
                {

                }
                return RedirectToAction("Publico", "Crud");
            }
        }

        public ActionResult Publico_edit(int? ID_Publico)
        {
            if (!ID_Publico.HasValue) { ID_Publico = 0; }
            List<T_Publico> lst = new List<T_Publico>();
            //string strSql = "SELECT ID_Publico,C_Publico,N_Estado"
            //        + " FROM T_Publico AS t"
            //        + " WHERE t.ID_Publico = " + ID_Publico.Value;
            SqlParameter IDPublicoParameter = new SqlParameter("@IDPublico", ID_Publico.Value);
            //lst = DB.T_Publico.SqlQuery(strSql).ToList();
            lst = DB.T_Publico.SqlQuery("[dbo].[PA_ExistePublico] @IDPublico", IDPublicoParameter).ToList();
            ViewBag.ID_Publico = lst.ElementAt(0).ID_Publico;
            ViewBag.C_Publico = lst.ElementAt(0).C_Publico;
            ViewBag.N_Estado = lst.ElementAt(0).N_Estado;
            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult Publico_delete(int? ID_Publico)
        {
            if (!ID_Publico.HasValue) { ID_Publico = 0; }
            ViewBag.ID_Publico = ID_Publico;
            return PartialView();
        }


        //Tipos de pregunta
        public ActionResult PreguntaTipo(int? ID_TipoPregunta)
        {
            List<T_TipoPregunta> lst = new List<T_TipoPregunta>();
            //string strSql = "SELECT ID_TipoPregunta, C_TipoPregunta, N_Estado FROM T_TipoPregunta ORDER BY C_TipoPregunta";
            //lst = DB.T_TipoPregunta.SqlQuery(strSql).ToList();
            lst = DB.T_TipoPregunta.SqlQuery("[dbo].[PA_TipoPregunta_List]").ToList();
            return View(lst);
        }

        [HttpPost]
        public ActionResult PreguntaTipo(int? ID_TipoPregunta, string C_TipoPregunta, bool? N_Estado, int isEdit, int isDelete)
        {
            var tPregunta = new T_TipoPregunta();
            if (isEdit == 1 || isDelete == 1)
            {
                tPregunta = new T_TipoPregunta { ID_TipoPregunta = ID_TipoPregunta.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                DB.T_TipoPregunta.Attach(tPregunta);
                if (isDelete == 1)
                {
                    DB.T_TipoPregunta.Remove(tPregunta);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_TipoPregunta.Add(tPregunta);
                        tPregunta.N_Estado = true;
                        tPregunta.A_CreacionUsuario = TempData["Usuario"].ToString();
                        tPregunta.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        if (!N_Estado.HasValue) { N_Estado = true; }
                        tPregunta.N_Estado = N_Estado.Value;
                        tPregunta.A_ModificacionUsuario = TempData["Usuario"].ToString();
                        tPregunta.A_ModificacionFecha = DateTime.Now;
                    }
                    tPregunta.C_TipoPregunta = C_TipoPregunta;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("PreguntaTipo", "Crud");
            }
        }

        public ActionResult PreguntaTipo_edit(int? ID_TipoPregunta)
        {
            if (!ID_TipoPregunta.HasValue) { ID_TipoPregunta = 0; }
            List<T_TipoPregunta> lst = new List<T_TipoPregunta>();
            //string strSql = "SELECT ID_TipoPregunta,C_TipoPregunta,N_Estado"
            //        + " FROM T_TipoPregunta AS tp"
            //        + " WHERE tp.ID_TipoPregunta = " + ID_TipoPregunta.Value;
            //lst = DB.T_TipoPregunta.SqlQuery(strSql).ToList();
            SqlParameter IDTipoPreguntaParameter = new SqlParameter("@IDTipoPregunta", ID_TipoPregunta.Value);
            //lst = DB.T_Publico.SqlQuery(strSql).ToList();
            lst = DB.T_TipoPregunta.SqlQuery("[dbo].[PA_ExisteTipoPregunta] @IDTipoPregunta", IDTipoPreguntaParameter).ToList();

            ViewBag.ID_TipoPregunta = lst.ElementAt(0).ID_TipoPregunta;
            ViewBag.C_TipoPregunta = lst.ElementAt(0).C_TipoPregunta;
            ViewBag.N_Estado = lst.ElementAt(0).N_Estado;
            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult PreguntaTipo_delete(int? ID_TipoPregunta)
        {
            if (!ID_TipoPregunta.HasValue) { ID_TipoPregunta = 0; }
            ViewBag.ID_TipoPregunta = ID_TipoPregunta;
            return PartialView();
        }

        //Secciones del cuestionario
        //public ActionResult Seccion(int? N_Cuestionario)
        public ActionResult Seccion(int? ID_Seccion)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }
            List<T_Seccion> lst = new List<T_Seccion>();
            lst = DB.ListaSeccionesUsuario(Usuario);
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            return View(lst);
        }

        [HttpPost]
        public ActionResult Seccion(int? ID_Seccion, string C_Seccion, bool? N_Estado, int isEdit, int isDelete)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }

            var tSeccion = new T_Seccion();
            if (isEdit == 1 || isDelete == 1)
            {
                tSeccion = new T_Seccion { ID_Seccion = ID_Seccion.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Seccion.Attach(tSeccion);
                if (isDelete == 1)
                {
                    DB.T_Seccion.Remove(tSeccion);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_Seccion.Add(tSeccion);
                        tSeccion.N_Estado = true;
                        tSeccion.A_CreacionUsuario = Usuario;
                        tSeccion.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        if (!N_Estado.HasValue) { N_Estado = true; }
                        tSeccion.N_Estado = N_Estado.Value;
                        tSeccion.A_ModificacionUsuario = Usuario;
                        tSeccion.A_ModificacionFecha = DateTime.Now;
                    }
                    tSeccion.C_Seccion = C_Seccion;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("Seccion", "Crud");
            }
        }

        public ActionResult Seccion_edit(int? ID_Seccion)
        {
            if (!ID_Seccion.HasValue) { ID_Seccion = 0; }
            List<T_Seccion> lst = new List<T_Seccion>();
            //string strSql = "SELECT ID_TipoPregunta,C_TipoPregunta,N_Estado"
            //        + " FROM T_TipoPregunta AS tp"
            //        + " WHERE tp.ID_TipoPregunta = " + ID_TipoPregunta.Value;
            //lst = DB.T_TipoPregunta.SqlQuery(strSql).ToList();
            SqlParameter IDSeccionParameter = new SqlParameter("@IDSeccion", ID_Seccion.Value);
            //lst = DB.T_Publico.SqlQuery(strSql).ToList();
            lst = DB.T_Seccion.SqlQuery("[dbo].[PA_ExisteSeccion] @IDSeccion", IDSeccionParameter).ToList();

            ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
            ViewBag.C_Seccion = lst.ElementAt(0).C_Seccion;
            ViewBag.N_Estado = lst.ElementAt(0).N_Estado;
            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult Seccion_delete(int? ID_Seccion)
        {
            if (!ID_Seccion.HasValue) { ID_Seccion = 0; }
            ViewBag.ID_Seccion = ID_Seccion;
            return PartialView();
        }

        //Secciones del cuestionario
        //public ActionResult Seccion(int? N_Cuestionario)
        public ActionResult Alternativa(int? ID_Alternativa)
        {
            //if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }
            //List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            List<T_Alternativa> lst = new List<T_Alternativa>();
            lst = DB.T_Alternativa.SqlQuery("[dbo].[PA_alternativa_List]").ToList();
            //lstCuestionario = DB.T_Cuestionario.Where(item => item.N_Estado == 10).ToList();
            //if (lstCuestionario.Count == 0) { lstCuestionario.Add(new T_Cuestionario() { ID_Cuestionario = 0, C_Titulo = "NO existen elementos a mostrar", N_Preguntas = 0 }); }
            //ViewBag.cuestionario = lstCuestionario;
            //ViewBag.N_Cuestionario = N_Cuestionario.Value;

            return View(lst);
        }

        [HttpPost]
        public ActionResult Alternativa(int? ID_Alternativa, string C_Alternativa, bool? N_Estado, int isEdit, int isDelete)
        {
            var tAlternativa = new T_Alternativa();
            if (isEdit == 1 || isDelete == 1)
            {
                tAlternativa = new T_Alternativa { ID_Alternativa = ID_Alternativa.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Alternativa.Attach(tAlternativa);
                if (isDelete == 1)
                {
                    DB.T_Alternativa.Remove(tAlternativa);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_Alternativa.Add(tAlternativa);
                        tAlternativa.N_Estado = true;
                        tAlternativa.A_CreacionUsuario = TempData["Usuario"].ToString();
                        tAlternativa.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        if (!N_Estado.HasValue) { N_Estado = true; }
                        tAlternativa.N_Estado = N_Estado.Value;
                        tAlternativa.A_ModificacionUsuario = TempData["Usuario"].ToString();
                        tAlternativa.A_ModificacionFecha = DateTime.Now;
                    }
                    tAlternativa.C_Alternativa = C_Alternativa;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("Alternativa", "Crud");
            }
        }

        public ActionResult Alternativa_edit(int? ID_Alternativa)
        {
            if (!ID_Alternativa.HasValue) { ID_Alternativa = 0; }
            List<T_Alternativa> lst = new List<T_Alternativa>();
            //string strSql = "SELECT ID_TipoPregunta,C_TipoPregunta,N_Estado"
            //        + " FROM T_TipoPregunta AS tp"
            //        + " WHERE tp.ID_TipoPregunta = " + ID_TipoPregunta.Value;
            //lst = DB.T_TipoPregunta.SqlQuery(strSql).ToList();
            SqlParameter IDAlternativaParameter = new SqlParameter("@IDAlternativa", ID_Alternativa.Value);
            //lst = DB.T_Publico.SqlQuery(strSql).ToList();
            lst = DB.T_Alternativa.SqlQuery("[dbo].[PA_ExisteAlternativa] @IDAlternativa", IDAlternativaParameter).ToList();

            ViewBag.ID_Alternativa = lst.ElementAt(0).ID_Alternativa;
            ViewBag.C_Alternativa = lst.ElementAt(0).C_Alternativa;
            ViewBag.N_Estado = lst.ElementAt(0).N_Estado;
            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult Alternativa_delete(int? ID_Alternativa)
        {
            if (!ID_Alternativa.HasValue) { ID_Alternativa = 0; }
            ViewBag.ID_Alternativa = ID_Alternativa;
            return PartialView();
        }

        //[HttpPost]
        //public ActionResult Seccion(int? ID_Seccion, int? N_Cuestionario, string C_Seccion,  int isEdit, int isDelete)
        //{
        //    if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

        //    var tabla = new T_Seccion();
        //    if (isEdit == 1 || isDelete == 1)
        //    {
        //        tabla = new T_Seccion { ID_Seccion = ID_Seccion.Value };
        //    }

        //    using (var DB = new CTX_Encuesta())
        //    {
        //        DB.T_Seccion.Attach(tabla);
        //        if (isDelete == 1)
        //        {
        //            DB.T_Seccion.Remove(tabla);
        //        }
        //        else
        //        {
        //            if (isEdit == 0)
        //            {
        //                DB.T_Seccion.Add(tabla);
        //            }
        //            tabla.N_Cuestionario = N_Cuestionario.Value;
        //            tabla.C_Seccion = C_Seccion;
        //            DB.Configuration.ValidateOnSaveEnabled = false;
        //        }
        //        try
        //        {
        //            DB.SaveChanges();
        //            ViewBag.N_Cuestionario = N_Cuestionario.Value;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        return RedirectToAction("Seccion", "Crud", new { N_Cuestionario = N_Cuestionario.Value });
        //    }
        //}

        //public ActionResult Seccion_datos(int? pCuestionario)
        //{
        //    if (!pCuestionario.HasValue) { pCuestionario = 0; }
        //    List<T_Seccion> lst = new List<T_Seccion>();
        //    string strSql = "SELECT ID_Seccion,p.C_Seccion,N_Cuestionario"
        //            + " FROM T_Seccion AS p"
        //            + " WHERE N_Cuestionario = " + pCuestionario.Value
        //            + " ORDER BY C_Seccion";
        //    lst = DB.T_Seccion.SqlQuery(strSql).ToList();

        //    return PartialView(lst);
        //}

        //public ActionResult Seccion_new(int N_Cuestionario)
        //{
        //    List<T_Cuestionario> lst = new List<T_Cuestionario>();

        //    lst = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();

        //    ViewBag.N_Cuestionario = N_Cuestionario;

        //    return PartialView();
        //}

        //public ActionResult Seccion_edit(int? ID_Seccion, int? N_Cuestionario)
        //{
        //    if (!ID_Seccion.HasValue) { ID_Seccion = 0; }

        //    List<T_Seccion> lst = new List<T_Seccion>();

        //    lst = DB.T_Seccion.Where(item => item.ID_Seccion == ID_Seccion.Value).ToList();

        //    ViewBag.ID_Seccion = ID_Seccion.Value;
        //    ViewBag.C_Seccion = lst.ElementAt(0).C_Seccion;
        //    ViewBag.N_Cuestionario = N_Cuestionario.Value;

        //    return PartialView(lst);
        //}

        //public ActionResult Seccion_delete(int? N_Cuestionario, int? ID_Seccion)
        //{
        //    if (!ID_Seccion.HasValue) { ID_Seccion = 0; }
        //    if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }
        //    ViewBag.N_Cuestionario = N_Cuestionario;
        //    ViewBag.ID_Seccion = ID_Seccion;
        //    return PartialView();
        //}
        ////

    }
}