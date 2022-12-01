using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using encuesta.Contexto;
//using encuesta.Contexto;
//using encuesta.FIlters;
using encuesta.Models;

namespace encuesta.Controllers
{
    //[TokenFilter]
    public class CuestionarioController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }
            List<T_Cuestionario> lst = new List<T_Cuestionario>();
            lst = DB.ListaCuestionariosUsuario(Usuario);

            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            return View(lst);
        }

        [HttpPost]
        //public ActionResult Registro(int? ID_Cuestionario, string C_Cuestionario, int? N_Publico, DateTime? D_Inicio, DateTime? D_Final, int? N_Preguntas, int? N_Estado, int isEdit, int isDelete)
        public ActionResult Registro(int? ID_Cuestionario, string C_Cuestionario, int? N_Area, int? N_Publico, string C_Ano, DateTime? D_Inicio, DateTime? D_Final, string C_Presentacion, string C_Resumen, string C_CorreoRemitente, int isEdit, int isDelete)
        {
            string datos = "";
            try
            {
                string Usuario = "";
                if (TempData.ContainsKey("Usuario"))
                {
                    Usuario = TempData["Usuario"].ToString();
                    datos = Usuario + ",";
                }
                if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }

                var tabla = new T_Cuestionario();
                if (isEdit == 1 || isDelete == 1)
                {
                    tabla = new T_Cuestionario { ID_Cuestionario = ID_Cuestionario.Value };
                }
                datos = datos + ID_Cuestionario.Value + ",";
                using (var DB = new CTX_Encuesta())
                {
                    DB.T_Cuestionario.Attach(tabla);
                    if (isDelete == 1)
                    {
                        tabla.ID_Estado = 5; //Anulado
                        tabla.A_ModificacionUsuario = Usuario;
                        tabla.A_ModificacionFecha = DateTime.Now;
                        //DB.T_Cuestionario.Remove(tabla);
                    }
                    else
                    {
                        if (isEdit == 0)
                        {
                            DB.T_Cuestionario.Add(tabla);
                            tabla.N_NroPreguntas = 0;
                            tabla.ID_Estado = 1; //Registrado
                            tabla.A_CreacionUsuario = Usuario;
                            tabla.A_CreacionFecha = DateTime.Now;
                        }
                        else
                        {
                            //tabla.N_Estado = N_Estado.Value;
                            tabla.A_ModificacionUsuario = Usuario;
                            tabla.A_ModificacionFecha = DateTime.Now;
                        }
                        tabla.ID_Area = N_Area.Value;
                        tabla.ID_Publico = N_Publico.Value;
                        tabla.C_Cuestionario = C_Cuestionario;
                        tabla.C_Presentacion = C_Presentacion;
                        tabla.C_Resumen = C_Resumen;
                        tabla.D_FechaInicio = D_Inicio.Value;
                        tabla.D_FechaFinal = D_Final.Value;
                        tabla.C_Ano = C_Ano;
                        tabla.C_CorreoRemitente = C_CorreoRemitente;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        datos = datos + DateTime.Now.ToString() + "," + N_Area.Value + "," + N_Publico.Value + "," + C_Cuestionario + "," + C_Presentacion + "," + C_Resumen + "," + D_Inicio.Value + "," + D_Final.Value + "," + C_Ano + "," + C_CorreoRemitente + ",";
                    }
                    try
                    {
                        DB.SaveChanges();
                        DB.Dispose();
                    }
                    catch (Exception ex)
                    {
                        datos = datos + ex.Message;
                        //ELog.save(this, datos);
                    }
                }
            }
            catch (Exception ex)
            {
                datos = datos + ex.Message + "|" + ex.InnerException + "|" + ex.Source + "|" + ex.StackTrace + "|" + ex.TargetSite;
               // ELog.save(this, datos);
            }
            return RedirectToAction("Registro", "Cuestionario");
        }

        public ActionResult Registro_new()
        {
            List<T_Publico> lstPublico = new List<T_Publico>();
            List<T_Area> lstArea = new List<T_Area>();
            List<T_Estado> lstEstado = new List<T_Estado>();

            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            TempData["Area"] = lstArea;

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();

            TempData["Publico"] = lstPublico;

            //lstEstado = DB.T_Estado.Where(item => item.C_Tabla == "T_Cuestionario").ToList();
            //TempData["Estado"] = lstEstado;

            return PartialView();
        }

        public ActionResult Registro_edit(int? ID_Cuestionario)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }

            List<T_Cuestionario> lst = new List<T_Cuestionario>();
            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            List<T_Estado> lstEstado = new List<T_Estado>();

            lst = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value).ToList();

            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            TempData["Area"] = lstArea;

            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            TempData["Publico"] = lstPublico;

            //lstEstado = DB.T_Estado.ToList();
            //TempData["Estado"] = lstEstado;

            ViewBag.ID_Cuestionario = lst.ElementAt(0).ID_Cuestionario;
            ViewBag.C_Cuestionario = lst.ElementAt(0).C_Cuestionario;
            ViewBag.D_Inicio = lst.ElementAt(0).D_FechaInicio;
            ViewBag.D_Final = lst.ElementAt(0).D_FechaFinal;
            ViewBag.N_Publico = lst.ElementAt(0).ID_Publico;
            ViewBag.N_Area = lst.ElementAt(0).ID_Area;
            ViewBag.C_Ano = lst.ElementAt(0).C_Ano;
            ViewBag.N_Preguntas = lst.ElementAt(0).N_NroPreguntas;
            ViewBag.C_Estado = lst.ElementAt(0).T_Estado.C_Estado;
            ViewBag.C_Presentacion = lst.ElementAt(0).C_Presentacion;
            ViewBag.C_Resumen = lst.ElementAt(0).C_Resumen;
            ViewBag.C_CorreoRemitente = lst.ElementAt(0).C_CorreoRemitente;

            if (lst.Count > 0)
            {

            }
            return PartialView(lst);
        }

        public ActionResult Registro_delete(int? ID_Cuestionario)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            ViewBag.ID_Cuestionario = ID_Cuestionario;
            return PartialView();
        }

        //public ActionResult Registro_seccion(int? ID_Cuestionario)
        //{
        //    string Usuario = "";
        //    if (TempData.ContainsKey("Usuario"))
        //    {
        //        Usuario = TempData["Usuario"].ToString();
        //    }
        //    if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }

        //    List<T_Cuestionario> lst = new List<T_Cuestionario>();
        //    List<T_Seccion> lstSeccion = new List<T_Seccion>();
        //    List<T_Config_Cuestionario> lstConfig = new List<T_Config_Cuestionario>();
        //    lst = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value).ToList();

        //    lstSeccion = DB.T_Seccion.Where(item => item.N_Estado == true && item.A_CreacionUsuario == Usuario).ToList();
        //    TempData["Seccion"] = lstSeccion;
        //    TempData["Usuario"] = Usuario;
        //    TempData.Keep("Usuario");

        //    lstConfig = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value && item.ID_Pregunta == 0).ToList();

        //    ViewBag.ID_Cuestionario = ID_Cuestionario.Value;
        //    ViewBag.C_Cuestionario = lst.ElementAt(0).C_Cuestionario;
        //    ViewBag.nSecciones = lstConfig.Count();
        //    return PartialView(lst);
        //}

        //public void _insertConfigSeccion(int pCuestionario, int pSeccion)
        //{
        //    string Usuario = "";
        //    if (TempData.ContainsKey("Usuario"))
        //    {
        //        Usuario = TempData["Usuario"].ToString();
        //    }

        //    List<T_Config_Cuestionario> lstConfig = new List<T_Config_Cuestionario>();
        //    lstConfig = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Pregunta == 0).ToList();

        //    var tabla = new T_Config_Cuestionario();
        //    using (var DB = new CTX_Encuesta())
        //    {
        //        DB.T_Config_Cuestionario.Attach(tabla);
        //        DB.T_Config_Cuestionario.Add(tabla);
        //        tabla.ID_Cuestionario = pCuestionario;
        //        tabla.ID_Seccion = pSeccion;
        //        tabla.ID_Pregunta = 0;
        //        tabla.ID_Alternativa = 0;
        //        tabla.N_OrdenSeccion = lstConfig.Count() + 1;
        //        tabla.A_CreacionUsuario = Usuario;
        //        tabla.A_CreacionFecha = DateTime.Now;
        //        DB.Configuration.ValidateOnSaveEnabled = false;
        //        try
        //        {
        //            DB.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Msg"] = ex.Message;
        //        }
        //    }
        //    TempData["Usuario"] = Usuario;
        //    TempData.Keep("Usuario");
        //}

        //public ActionResult ConfiguraSecciones(int? ID_Cuestionario)
        //{
        //    List<T_Config_Cuestionario> lstConfig = new List<T_Config_Cuestionario>();
        //    lstConfig = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value && item.ID_Pregunta == 0).OrderBy(item => item.N_OrdenSeccion).ToList();
        //    return PartialView(lstConfig);
        //}

        //public void _deleteConfigSeccion(int pCuestionario, int pSeccion, int pCant, int pOrden)
        //{
        //    List<T_Config_Cuestionario> lst = new List<T_Config_Cuestionario>();
        //    lst = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Pregunta == 0).ToList();

        //    using (var DB = new CTX_Encuesta())
        //    {
        //        var flg = DB.EliminarConfiguracion(pCuestionario, pSeccion);

        //        if (flg == 1 && lst.Count > 1)
        //        {
        //            List<T_Config_Cuestionario> lst2 = new List<T_Config_Cuestionario>();
        //            lst2 = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Pregunta == 0 && item.N_OrdenSeccion > pOrden).OrderBy(item => item.N_OrdenSeccion).ToList();

        //            for (int i = 0; i < lst2.Count(); i++)
        //            {
        //                var tabla2 = new T_Config_Cuestionario();
        //                tabla2 = new T_Config_Cuestionario { ID_Cuestionario = pCuestionario, ID_Seccion = lst2.ElementAt(i).ID_Seccion };
        //                using (var DB2 = new CTX_Encuesta())
        //                {
        //                    DB2.T_Config_Cuestionario.Attach(tabla2);
        //                    tabla2.N_OrdenSeccion = lst2.ElementAt(i).N_OrdenSeccion - 1;
        //                    try
        //                    {
        //                        DB2.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        TempData["Msg"] = ex.Message;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //public ActionResult Registro_pregunta(int? ID_Cuestionario)
        //{
        //    string Usuario = "";
        //    if (TempData.ContainsKey("Usuario"))
        //    {
        //        Usuario = TempData["Usuario"].ToString();
        //    }

        //    if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }

        //    List<T_Cuestionario> lst = new List<T_Cuestionario>();
        //    List<T_Seccion> lstSeccion = new List<T_Seccion>();
        //    List<T_ClasificacionPregunta> lstClasificacion = new List<T_ClasificacionPregunta>();
        //    List<T_Pregunta> lstPregunta = new List<T_Pregunta>();

        //    lst = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value).ToList();
        //    lstSeccion = DB.ListConfigCuestionarioSecciones(ID_Cuestionario.Value);
        //    lstClasificacion = DB.T_ClasificacionPregunta.Where(item => item.N_Estado == true && item.A_CreacionUsuario == Usuario).OrderByDescending(item => item.ID_ClasificacionPregunta).ToList();

        //    TempData["Seccion"] = lstSeccion;
        //    TempData["Clasificacion"] = lstClasificacion;
        //    TempData["Usuario"] = Usuario;
        //    TempData.Keep("Usuario");

        //    ViewBag.ID_Cuestionario = ID_Cuestionario.Value;
        //    ViewBag.C_Cuestionario = lst.ElementAt(0).C_Cuestionario;
        //    if(lstSeccion.Count > 0) {ViewBag.ID_Seccion = lstSeccion.ElementAt(0).ID_Seccion; } else { ViewBag.ID_Seccion = 0; }
        //    ViewBag.ID_Clasificacion = lstClasificacion.ElementAt(0).ID_ClasificacionPregunta;
        //    //ViewBag.nSecciones = lstConfig.Count();
        //    return PartialView(lst);
        //}

        //public void _insertConfigPregunta(int pCuestionario, int pSeccion, int pPregunta)
        //{
        //    string Usuario = "";
        //    if (TempData.ContainsKey("Usuario"))
        //    {
        //        Usuario = TempData["Usuario"].ToString();
        //    }

        //    List<T_Config_Cuestionario> lstConfigSeccion = new List<T_Config_Cuestionario>();
        //    lstConfigSeccion = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Seccion == pSeccion && item.ID_Pregunta == 0 && item.ID_Alternativa == 0).ToList();

        //    List<T_Config_Cuestionario> lstConfig = new List<T_Config_Cuestionario>();
        //    lstConfig = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Pregunta != 0 && item.ID_Alternativa == 0).ToList();

        //    var tabla = new T_Config_Cuestionario();
        //    using (var DB2 = new CTX_Encuesta())
        //    {
        //        DB2.T_Config_Cuestionario.Attach(tabla);
        //        DB2.T_Config_Cuestionario.Add(tabla);
        //        tabla.ID_Cuestionario = pCuestionario;
        //        tabla.ID_Seccion = pSeccion;
        //        tabla.ID_Pregunta = pPregunta;
        //        tabla.ID_Alternativa = 0;
        //        tabla.N_OrdenSeccion = lstConfigSeccion.ElementAt(0).N_OrdenSeccion;
        //        tabla.N_OrdenPreg = lstConfig.Count() + 1;
        //        tabla.A_CreacionUsuario = Usuario;
        //        tabla.A_CreacionFecha = DateTime.Now;
        //        DB2.Configuration.ValidateOnSaveEnabled = false;
        //        try
        //        {
        //            DB2.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Msg"] = ex.Message;
        //        }
        //    }
        //    //Graba la las alternativas correspondientes a la pregunta
        //    DB.InsertarAlternativasPregunta(pCuestionario, pSeccion, pPregunta);
        //    TempData["Usuario"] = Usuario;
        //    TempData.Keep("Usuario");
        //}

        //public ActionResult ConfiguraPreguntas(int? ID_Cuestionario, int ID_Seccion)
        //{
        //    List<T_Config_Cuestionario> lstConfig = new List<T_Config_Cuestionario>();
        //    lstConfig = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario.Value && item.ID_Seccion == ID_Seccion && item.ID_Pregunta != 0 && item.ID_Alternativa == 0).OrderBy(item => item.N_OrdenPreg).ToList();
        //    return PartialView(lstConfig);
        //}

        //public void _deleteConfigPregunta(int pCuestionario, int pSeccion, int pPregunta, int pOrden)
        //{
        //    using (var DB = new CTX_Encuesta())
        //    {
        //        var flg = DB.EliminarConfiguracion(pCuestionario, pSeccion, pPregunta);

        //        if (flg == 1)
        //        {
        //            List<T_Config_Cuestionario> lst = new List<T_Config_Cuestionario>();
        //            lst = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario && item.ID_Pregunta != pPregunta && item.N_OrdenPreg > pOrden).OrderBy(item => item.N_OrdenPreg).ToList();

        //            for (int i = 0; i < lst.Count(); i++)
        //            {
        //                var tabla2 = new T_Config_Cuestionario();
        //                tabla2 = new T_Config_Cuestionario { ID_Cuestionario = pCuestionario, ID_Seccion = lst.ElementAt(i).ID_Seccion, ID_Pregunta = lst.ElementAt(i).ID_Pregunta, ID_Alternativa = lst.ElementAt(i).ID_Alternativa};
        //                using (var DB2 = new CTX_Encuesta())
        //                {
        //                    DB2.T_Config_Cuestionario.Attach(tabla2);
        //                    tabla2.N_OrdenPreg = lst.ElementAt(i).N_OrdenPreg - 1;
        //                    try
        //                    {
        //                        DB2.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        TempData["Msg"] = ex.Message;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //[HttpGet]
        //public JsonResult CargarCuestionarios(int? ID_Area, int? ID_Publico)
        //{
        //    if (!ID_Area.HasValue) { ID_Area = 0; }
        //    if (!ID_Publico.HasValue) { ID_Publico = 0; }
        //    string Usuario = "";
        //    if (TempData.ContainsKey("Usuario"))
        //    {
        //        Usuario = TempData["Usuario"].ToString();
        //    }
        //    List<T_Cuestionario> lst = new List<T_Cuestionario>();
        //    if (Usuario == "MultiencuestaDAMER")
        //    {
        //        lst = DB.T_Cuestionario.Where(item => item.ID_Area == ID_Area.Value && item.ID_Publico == ID_Publico.Value && item.ID_Estado < 4 && item.A_CreacionUsuario != "SArizaca").OrderByDescending(item => new { item.ID_Cuestionario }).ToList();

        //    }else{
        //        lst = DB.T_Cuestionario.Where(item => item.ID_Area == ID_Area.Value && item.ID_Publico == ID_Publico.Value && item.ID_Estado < 4 && item.A_CreacionUsuario == Usuario).OrderByDescending(item => new { item.ID_Cuestionario }).ToList();
        //    }

        //    TempData["Usuario"] = Usuario;
        //    TempData.Keep("Usuario");
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Configuracion_datos(int? pCuestionario)
        //{
        //    if (!pCuestionario.HasValue) { pCuestionario = 0; }
        //    List<T_Config_Cuestionario> lst = new List<T_Config_Cuestionario>();
        //    lst = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario).OrderBy(item => new { item.N_OrdenSeccion, item.N_OrdenPreg, item.N_Orden }).ToList();
        //    return PartialView(lst);
        //}

        //public ActionResult ConfPregunta_edit(int ID_Cuestionario, int ID_Seccion, int ID_Pregunta)
        //{

        //    List<T_Config_Cuestionario> lst = new List<T_Config_Cuestionario>();

        //    lst = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario && item.ID_Seccion == ID_Seccion && item.ID_Pregunta == ID_Pregunta && item.ID_Alternativa == 0).ToList();

        //    ViewBag.ID_Cuestionario = lst.ElementAt(0).ID_Cuestionario;
        //    ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
        //    ViewBag.ID_Pregunta = lst.ElementAt(0).ID_Pregunta;
        //    ViewBag.C_Seccion = lst.ElementAt(0).T_Seccion.C_Seccion;
        //    ViewBag.C_Pregunta = lst.ElementAt(0).T_Pregunta.C_Pregunta;
        //    ViewBag.C_TipoPregunta = lst.ElementAt(0).T_Pregunta.T_TipoPregunta.C_TipoPregunta;
        //    ViewBag.ID_TipoPregunta = lst.ElementAt(0).T_Pregunta.T_TipoPregunta.ID_TipoPregunta;

        //    if (ViewBag.ID_TipoPregunta == 4)
        //    {
        //        List<T_TipoLikert> lst2 = new List<T_TipoLikert>();
        //        lst2 = DB.T_TipoLikert.Where(item => item.N_Estado == true).ToList();
        //        ViewBag.Tipo_Likert = lst2.ToList();
        //        if (lst.ElementAt(0).ID_TipoLikert == null)
        //        {
        //            ViewBag.ID_TipoLikert = lst2.ElementAt(0).ID_TipoLikert;
        //        }
        //        else
        //        {
        //            ViewBag.ID_TipoLikert = lst.ElementAt(0).ID_TipoLikert;
        //        }
        //    }
        //    ViewBag.N_PregNoIncluyeEmisionBoleto = lst.ElementAt(0).N_PregNoIncluyeEmisionBoleto;
        //    ViewBag.N_PregEmisionBoleto = lst.ElementAt(0).N_PregEmisionBoleto;

        //    if (lst.ElementAt(0).N_PregComentario == null)
        //    {
        //        ViewBag.N_PregComentario = 0;
        //    }
        //    else
        //    {
        //        if (lst.ElementAt(0).N_PregComentario == true)
        //        {
        //            ViewBag.N_PregComentario = 1;
        //        }
        //        else
        //        {
        //            ViewBag.N_PregComentario = 0;
        //        }

        //    }

        //    if (lst.ElementAt(0).N_PregSecuencia == null)
        //    {
        //        ViewBag.N_PregSecuencia = 0;
        //    }
        //    else
        //    {
        //        if (lst.ElementAt(0).N_PregSecuencia == true)
        //        {
        //            ViewBag.N_PregSecuencia = 1;
        //        }
        //        else
        //        {
        //            ViewBag.N_PregSecuencia = 0;
        //        }
        //    }

        //    ViewBag.N_PregComentarioObligatorio = lst.ElementAt(0).N_PregComentarioObligatorio;
        //    ViewBag.N_PregAnterior = lst.ElementAt(0).N_PregAnterior;
        //    ViewBag.N_PregSiguiente = lst.ElementAt(0).N_PregSiguiente;
        //    ViewBag.N_FinalizaEncuesta = lst.ElementAt(0).N_FinalizaEncuesta;

        //    return PartialView(lst);
        //}

        //public ActionResult ConfAlternativa_edit(int ID_Cuestionario, int ID_Seccion, int ID_Pregunta, int ID_Alternativa)
        //{

        //    List<T_Config_Cuestionario> lst = new List<T_Config_Cuestionario>();

        //    lst = DB.T_Config_Cuestionario.Where(item => item.ID_Cuestionario == ID_Cuestionario && item.ID_Seccion == ID_Seccion && item.ID_Pregunta == ID_Pregunta && item.ID_Alternativa == ID_Alternativa).ToList();

        //    ViewBag.ID_Cuestionario = lst.ElementAt(0).ID_Cuestionario;
        //    ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
        //    ViewBag.ID_Pregunta = lst.ElementAt(0).ID_Pregunta;
        //    ViewBag.ID_Alternativa = lst.ElementAt(0).ID_Alternativa;
        //    ViewBag.C_Seccion = lst.ElementAt(0).T_Seccion.C_Seccion;
        //    ViewBag.C_Pregunta = lst.ElementAt(0).T_Pregunta.C_Pregunta;
        //    ViewBag.C_Alternativa = lst.ElementAt(0).T_Alternativa.C_Alternativa;

        //    if (lst.ElementAt(0).N_AltComentario == null)
        //    {
        //        ViewBag.N_AltComentario = 0;
        //    }
        //    else
        //    {
        //        if(lst.ElementAt(0).N_AltComentario == true)
        //        {
        //            ViewBag.N_AltComentario = 1;
        //        }
        //        else
        //        {
        //            ViewBag.N_AltComentario = 0;
        //        }

        //    }

        //    if (lst.ElementAt(0).N_AltSecuencia == null)
        //    {
        //        ViewBag.N_AltSecuencia = 0;
        //    }
        //    else
        //    {
        //        if (lst.ElementAt(0).N_AltSecuencia == true)
        //        {
        //            ViewBag.N_AltSecuencia = 1;
        //        }
        //        else
        //        {
        //            ViewBag.N_AltSecuencia = 0;
        //        }
        //    }
        //    ViewBag.N_AltComentarioObligatorio = lst.ElementAt(0).N_AltComentarioObligatorio;
        //    ViewBag.N_AltPregAnterior = lst.ElementAt(0).N_AltPregAnterior;
        //    ViewBag.N_AltPregSiguiente = lst.ElementAt(0).N_AltPregSiguiente;
        //    ViewBag.N_FinalizaEncuesta = lst.ElementAt(0).N_FinalizaEncuesta;
        //    ViewBag.ID_TipoLikert = lst.ElementAt(0).ID_TipoLikert;

        //    var ID = lst.ElementAt(0).ID_TipoLikert;

        //    if (ID != null)
        //    {
        //        List<T_ConfigAlternativaEscala> lst2 = new List<T_ConfigAlternativaEscala>();
        //        lst2 = DB.ListConfigAlternativaEscala(ID_Pregunta, ID_Alternativa, ID.Value);
        //        ViewBag.T_ConfigAlternativaEscala = lst2.ToList();
        //    }

        //    return PartialView(lst);
        //}

        //[HttpPost]
        //public JsonResult Registra_orden(List<T_ConfiguracionOrden> pConfiguracionOrden)
        //{
        //    List<T_ConfiguracionOrden> lst = new List<T_ConfiguracionOrden>();

        //    for (int i = 0; i < pConfiguracionOrden.Count; i++)
        //    {
        //        lst = DB.RegistraConfiguracionOrden(pConfiguracionOrden.ElementAt(i));
        //    }

        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
    }
}