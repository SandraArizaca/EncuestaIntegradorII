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
    public class PreguntaController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Registro(int? N_Cuestionario)
        //{
        //    if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }
        //    List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
        //    lstCuestionario = DB.T_Cuestionario.Where(item => item.N_Estado == 10).ToList();
        //    if (lstCuestionario.Count == 0) { lstCuestionario.Add(new T_Cuestionario() { ID_Cuestionario = 0, C_Titulo = "NO existen elementos a mostrar" , N_Preguntas = 0}); }
        //    ViewBag.cuestionario = lstCuestionario;

        //    ViewBag.N_Cuestionario = N_Cuestionario.Value;

        //    return View();

        //}

        public ActionResult Registro(int? ID_Pregunta)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }
            List<T_Pregunta> lst = new List<T_Pregunta>();
            lst = DB.ListaPreguntasUsuario(Usuario);
            
            ViewBag.C_Usuario = Usuario; //TempData["Usuario"].ToString();
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            return View(lst);

        }

        [HttpPost]
        public ActionResult Registro(int? ID_Pregunta, int? N_Clasificacion, int? N_TipoPregunta, string C_Pregunta, int? N_NivelValoracion, bool? N_Estado, int isEdit, int isDelete)
        {
            //if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            var tabla = new T_Pregunta();
            if (isEdit == 1 || isDelete == 1)
            {
                tabla = new T_Pregunta { ID_Pregunta = ID_Pregunta.Value };
            }

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Pregunta.Attach(tabla);
                if (isDelete == 1)
                {
                    DB.T_Pregunta.Remove(tabla);
                }
                else
                {
                    if (isEdit == 0)
                    {
                        DB.T_Pregunta.Add(tabla);
                        tabla.N_Estado = true;
                        tabla.A_CreacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_CreacionFecha = DateTime.Now;
                    }
                    else
                    {
                        tabla.N_Estado = true;
                        tabla.A_ModificacionUsuario = TempData["Usuario"].ToString();
                        tabla.A_ModificacionFecha = DateTime.Now;
                    }
                    tabla.ID_ClasificacionPregunta = N_Clasificacion.Value;
                    tabla.ID_TipoPregunta = N_TipoPregunta.Value;
                    tabla.C_Pregunta = C_Pregunta;
                    tabla.N_NivelValoracion = N_NivelValoracion.Value;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Registro", "Pregunta");
            }
        }

        //public ActionResult Registro_datos(int? pCuestionario)
        //{
        //    if (!pCuestionario.HasValue) { pCuestionario = 0; }
        //    List<TB_Pregunta> lst = new List<TB_Pregunta>();
        //    string strSql = "SELECT ID_Pregunta,p.N_Seccion,p.N_Orden,p.C_Pregunta,C_TipoPregunta,p.B_Obligatoria,p.N_Ultimo,p.N_TipoPregunta,N_Preguntas,p.N_Estado"
        //            + " FROM T_Pregunta AS p"
        //            + " INNER JOIN T_TipoPregunta AS tp ON tp.ID_TipoPregunta = p.N_TipoPregunta"
        //            + " LEFT JOIN T_Seccion AS s ON s.ID_Seccion = p.N_Seccion"
        //            + " INNER JOIN T_Cuestionario AS c ON c.ID_Cuestionario = s.N_Cuestionario"
        //            + " WHERE ID_Cuestionario = " + pCuestionario.Value
        //            + " ORDER BY N_Orden";
        //    lst = DB.TB_Pregunta.SqlQuery(strSql).ToList();
        //    if (lst.Count > 0)
        //    {
        //        ViewBag.N_Preguntas = lst.ElementAt(0).N_Preguntas;
        //    }else
        //    {
        //        ViewBag.N_Preguntas = 1;
        //    }

        //    List<T_Orden> lst2 = new List<T_Orden>();
        //    string strSql2 = "SELECT COUNT(ID_Pregunta) AS N_Orden FROM T_Pregunta WHERE N_Seccion IN"
        //        + " (SELECT ID_Seccion FROM T_Cuestionario AS c "
        //        + " INNER JOIN T_Seccion AS s ON c.ID_Cuestionario=s.N_Cuestionario WHERE ID_Cuestionario=" + pCuestionario.Value + ")";
        //    lst2 = DB.T_Orden.SqlQuery(strSql2).ToList();

        //    ViewBag.N_Pregunta = lst2.ElementAt(0).N_Orden;
        //    ViewBag.N_Cuestionario = pCuestionario.Value;

        //    return PartialView(lst);
        //}

        public ActionResult Registro_new()
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }

            List<T_TipoPregunta> lstTipo = new List<T_TipoPregunta>();
            List<T_ClasificacionPregunta> lstClasificacion = new List<T_ClasificacionPregunta>();

            lstTipo = DB.T_TipoPregunta.Where(item => item.N_Estado == true).ToList();
            TempData["Tipo"] = lstTipo;

            lstClasificacion = DB.T_ClasificacionPregunta.Where(item => item.N_Estado == true && item.A_CreacionUsuario == Usuario).OrderByDescending(item => new { item.ID_ClasificacionPregunta}).ToList();
            TempData["Clasificacion"] = lstClasificacion;

            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");

            return PartialView();
        }

        public ActionResult Registro_edit(int? ID_Pregunta)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }

            if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }
            //if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            List<T_Pregunta> lst = new List<T_Pregunta>();
            List<T_TipoPregunta> lstTipo = new List<T_TipoPregunta>();
            List<T_ClasificacionPregunta> lstClasificacion = new List<T_ClasificacionPregunta>();

            lst = DB.T_Pregunta.Where(item => item.ID_Pregunta == ID_Pregunta).ToList();

            lstTipo = DB.T_TipoPregunta.Where(item => item.N_Estado == true).ToList();
            TempData["Tipo"] = lstTipo;

            lstClasificacion = DB.T_ClasificacionPregunta.Where(item => item.N_Estado == true && item.A_CreacionUsuario == Usuario).OrderByDescending(item => new { item.ID_ClasificacionPregunta }).ToList();
            TempData["Clasificacion"] = lstClasificacion;

            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            
            ViewBag.ID_Pregunta = ID_Pregunta.Value;
            ViewBag.C_Pregunta = lst.ElementAt(0).C_Pregunta;
            ViewBag.N_TipoPregunta = lst.ElementAt(0).ID_TipoPregunta;
            ViewBag.N_Clasificacion = lst.ElementAt(0).ID_ClasificacionPregunta;
            ViewBag.N_NivelValoracion = lst.ElementAt(0).N_NivelValoracion;

            return PartialView(lst);
        }

        public ActionResult Registro_delete(int? ID_Pregunta)
        {
            if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }
            ViewBag.ID_Pregunta = ID_Pregunta;
            return PartialView();
        }

        public ActionResult Registro_alter(int? ID_Pregunta, string C_Usuario, int N_Mide)
        {
            if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }
            //if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            List<T_Pregunta> lst = new List<T_Pregunta>();
            //List<T_TipoPregunta> lstTipo = new List<T_TipoPregunta>();
            //List<T_Likert> lstLikert = new List<T_Likert>();

            lst = DB.T_Pregunta.Where(item => item.ID_Pregunta == ID_Pregunta.Value).ToList();
            //int nTipoPregunta = lst.ElementAt(0).N_TipoPregunta;

            //lstTipo = DB.T_TipoPregunta.Where(item => item.ID_TipoPregunta == nTipoPregunta).ToList();
            ViewBag.C_TipoPregunta = lst.ElementAt(0).T_TipoPregunta.C_TipoPregunta;

            ViewBag.N_Pregunta = ID_Pregunta.Value;
            ViewBag.C_Pregunta = lst.ElementAt(0).C_Pregunta;
            ViewBag.N_TipoPregunta = lst.ElementAt(0).ID_TipoPregunta;
            ViewBag.C_Usuario = C_Usuario;
            ViewBag.N_Mide = N_Mide;

            //ViewBag.N_Cuestionario = N_Cuestionario;

            //int nLikert = 0;
            //if (ViewBag.N_Likert > 0)
            //{
            //    nLikert = ViewBag.N_Likert;
            //    lstLikert = DB.T_Likert.Where(item => item.ID_Likert == nLikert).ToList();
            //    ViewBag.C_Likert = '[' + lstLikert.ElementAt(0).C_Likert + ']';
            //}
            //else
            //{
            //    ViewBag.C_Likert = "";
            //}

            //ViewBag.N_Inicio = 1;
            return PartialView(lst);
        }

        //public ActionResult Registro_confi(int? ID_Pregunta, int? N_Cuestionario)
        //{
        //    if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }

        //    List<T_Pregunta> lst = new List<T_Pregunta>();
        //    List<T_TipoPregunta> lstTipo = new List<T_TipoPregunta>();
        //    List<T_Likert> lstLikert = new List<T_Likert>();

        //    lst = DB.T_Pregunta.Where(item => item.ID_Pregunta == ID_Pregunta.Value).ToList();
        //    int nTipoPregunta = lst.ElementAt(0).N_TipoPregunta;

        //    lstTipo = DB.T_TipoPregunta.Where(item => item.ID_TipoPregunta == nTipoPregunta).ToList();
        //    ViewBag.C_TipoPregunta = lstTipo.ElementAt(0).C_TipoPregunta;

        //    ViewBag.N_Pregunta = ID_Pregunta.Value;
        //    ViewBag.C_Pregunta = lst.ElementAt(0).C_Pregunta;
        //    ViewBag.N_TipoPregunta = lst.ElementAt(0).N_TipoPregunta;
        //    ViewBag.N_Likert = lst.ElementAt(0).N_Likert;

        //    int nLikert = 0;
        //    if (ViewBag.N_Likert > 0)
        //    {
        //        nLikert = ViewBag.N_Likert;
        //        lstLikert = DB.T_Likert.Where(item => item.ID_Likert == nLikert).ToList();
        //        ViewBag.C_Likert = '[' + lstLikert.ElementAt(0).C_Likert + ']';
        //    }
        //    else
        //    {
        //        ViewBag.C_Likert = "";
        //    }

        //    ViewBag.N_Inicio = 1;
        //    return PartialView(lst);
        //}

        public ActionResult Alternativa(int? ID_Pregunta, string C_Usuario, int N_Mide)
        {
            if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }

            List<T_Alternativa> lst = new List<T_Alternativa>();
            lst = DB.T_Alternativa.Where(item => item.ID_Pregunta == ID_Pregunta.Value).OrderBy(item => item.N_Orden).ToList();
            ViewBag.nAlternativas = lst.Count();
            ViewBag.N_Pregunta = ID_Pregunta.Value;
            ViewBag.C_Usuario = C_Usuario;
            ViewBag.N_Mide = N_Mide;

            return PartialView(lst);
        }

        //public ActionResult Alternativa_multiple(int? ID_Pregunta)
        //{
        //    if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }

        //    List<T_Alternativa> lst = new List<T_Alternativa>();
        //    lst = DB.T_Alternativa.Where(item => item.N_Pregunta == ID_Pregunta.Value).ToList();
        //    ViewBag.nAlternativas = lst.Count();
        //    ViewBag.N_Pregunta = ID_Pregunta.Value;

        //    return PartialView(lst);
        //}

        //public ActionResult Alternativa_comentario()
        //{
        //    return PartialView();
        //}

        //public ActionResult Alternativa_likert(int? ID_Pregunta)
        //{
        //    if (!ID_Pregunta.HasValue) { ID_Pregunta = 0; }

        //    List<T_Alternativa> lst = new List<T_Alternativa>();
        //    lst = DB.T_Alternativa.Where(item => item.N_Pregunta == ID_Pregunta.Value).ToList();
        //    ViewBag.nAlternativas = lst.Count();
        //    ViewBag.N_Pregunta = ID_Pregunta.Value;

        //    return PartialView(lst);
        //}

        public void _updateAlternativa(int pID, string pAlternativa, int? pIntervalo, int pOrden, string pUsuario)
        {

            T_Alternativa obj = DB.T_Alternativa.Find(pID);

            try
            {
                obj.C_Alternativa = pAlternativa;
                obj.N_Intervalo = pIntervalo;
                obj.N_Orden = pOrden;
                obj.A_ModificacionUsuario = pUsuario;
                obj.A_ModificacionFecha = DateTime.Now;

                DB.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
            }

        }

        public int _insertAlternativa(int? pID, int pPregunta, string pAlternativa, int? pIntervalo, int pOrden, string pUsuario)
        {
            int ID = 0;
            var tabla = new T_Alternativa();
            using (var DB = new CTX_Encuesta())
            {
                DB.T_Alternativa.Attach(tabla);
                DB.T_Alternativa.Add(tabla);
                tabla.N_Estado = true;
                tabla.ID_Pregunta = pPregunta;
                tabla.C_Alternativa = pAlternativa;
                tabla.N_Intervalo = pIntervalo;
                tabla.N_Orden = pOrden;
                tabla.A_CreacionUsuario = pUsuario;
                tabla.A_CreacionFecha = DateTime.Now;
                DB.Configuration.ValidateOnSaveEnabled = false;
                try
                {
                    DB.SaveChanges();
                    ID = tabla.ID_Alternativa;
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message;
                }
            }
            return ID;
        }

        public int _deleteAlternativa(int pID)
        {
            //List<T_EncuestaDetalle> lst = DB.T_EncuestaDetalle.Where(item => item.N_Alternativa.Value == pID).ToList();
            //if (lst != null && lst.Count > 0)
            //{
            //    return 1;
            //}
            //else
            //{
            using (var DB = new CTX_Encuesta())
            {

                if (pID > 0) { 
                    T_Alternativa obj = DB.T_Alternativa.Find(pID);
                    var idpregunta = obj.ID_Pregunta;
                    var orden = obj.N_Orden;
                    List<T_Alternativa> lst = new List<T_Alternativa>();
                    lst = DB.T_Alternativa.Where(item => item.ID_Pregunta == idpregunta && item.N_Orden > orden).ToList();

                    for (int i = 0; i < lst.Count(); i++)
                    {
                        var tabla2 = new T_Alternativa();
                        tabla2 = new T_Alternativa { ID_Alternativa = lst.ElementAt(i).ID_Alternativa };
                        using (var DB2 = new CTX_Encuesta())
                        {
                            DB2.T_Alternativa.Attach(tabla2);
                            tabla2.N_Orden = lst.ElementAt(i).N_Orden - 1;
                            try
                            {
                                DB2.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                TempData["Msg"] = ex.Message;
                            }
                        }
                    }
                    var flg = DB.EliminarAlternativa(pID);
                }
            }
            return 0;
            //}
        }

        [HttpGet]
        public JsonResult ClasificarPreguntas(int? ID_Clasificacion)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }
            if (!ID_Clasificacion.HasValue) { ID_Clasificacion = 0; }

            List<T_Pregunta> lst = new List<T_Pregunta>();
            lst = DB.T_Pregunta.Where(item => item.ID_ClasificacionPregunta == ID_Clasificacion.Value && item.A_CreacionUsuario == Usuario).OrderByDescending(item => new { item.ID_Pregunta }).ToList();
            
            TempData["Pregunta"] = lst;
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult _alternativasPregunta(int? pPregunta)
        //{
        //    if (!pPregunta.HasValue) { pPregunta = 0; }
        //    List<TB_Alternativa> lstAlternativas = new List<TB_Alternativa>();
        //    string strSql = "SELECT a.ID_Alternativa,a.N_Pregunta,a.C_Alternativa,a.N_Peso,a.N_CantidadMinimaCaracteres,"
        //            + " a.N_CantidadMaximaCaracteres,a.B_NuevoOrden,a.B_Comentario,a.B_FinalizaEncuesta,C_TipoPregunta,N_TipoPregunta"
        //            + " FROM T_Alternativa AS a"
        //            + " INNER JOIN T_Pregunta AS p ON a.N_Pregunta = p.ID_Pregunta"
        //            + " INNER JOIN T_TipoPregunta as tp ON p.N_TipoPregunta = tp.ID_TipoPregunta"
        //            + " WHERE N_Pregunta = " + pPregunta.Value;

        //    try
        //    {
        //        ViewBag.cTipoPregunta = "";
        //        lstAlternativas = DB.TB_Alternativa.SqlQuery(strSql).ToList();
        //        if (lstAlternativas.Count > 0)
        //        {
        //            ViewBag.cTipoPregunta = lstAlternativas.ElementAt(0).C_TipoPregunta;
        //        }
        //        TempData["SqlFlag"] = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["SqlFlag"] = -1;
        //        TempData["Msg"] = ex.Message;
        //    }

        //    return PartialView(lstAlternativas);
        //}

        //public ActionResult PreguntaRegistro_alternativas()
        //{
        //    return PartialView();
        //}

        public ActionResult Configuracion(int? N_Cuestionario)
        {
            if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }
            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();
            //if (lstCuestionario.Count == 0) { lstCuestionario.Add(new T_Cuestionario() { ID_Cuestionario = 0, C_Cuestionario = "NO existen elementos a mostrar", N_NroPreguntas = 0 }); }

            if(lstCuestionario.Count == 0) {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }
            ViewBag.N_Cuestionario = N_Cuestionario;

            return View();

        }

        [HttpPost]
        public ActionResult Configuracion(int? N_Cuestionario, int? ID_Seccion, int? ID_Pregunta, int? ID_TipoLikert, bool? N_PregComentario, bool? N_PregComentarioObligatorio, bool? N_PregEmisionBoleto, bool? N_PregNoIncluyeEmisionBoleto, bool? N_FinalizaEncuesta, bool? N_PregSecuencia, int? N_PregAnterior, int? N_PregSiguiente)
        {

            var tabla = new T_Config_Cuestionario();
            tabla = new T_Config_Cuestionario { ID_Cuestionario = N_Cuestionario.Value, ID_Seccion = ID_Seccion.Value, ID_Pregunta = ID_Pregunta.Value, ID_Alternativa = 0 };

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Config_Cuestionario.Attach(tabla);

                tabla.ID_TipoLikert = ID_TipoLikert;
                tabla.N_FinalizaEncuesta = N_FinalizaEncuesta;
                tabla.N_PregAnterior = N_PregAnterior;
                tabla.N_PregComentario = N_PregComentario;
                tabla.N_PregComentarioObligatorio = N_PregComentarioObligatorio;
                tabla.N_PregEmisionBoleto = N_PregEmisionBoleto;
                tabla.N_PregNoIncluyeEmisionBoleto = N_PregNoIncluyeEmisionBoleto;
                tabla.N_PregSecuencia = N_PregSecuencia;
                tabla.N_PregSiguiente = N_PregSiguiente;
                tabla.A_ModificacionUsuario = TempData["Usuario"].ToString();
                tabla.A_ModificacionFecha = DateTime.Now;


                DB.Configuration.ValidateOnSaveEnabled = false;

                try
                {
                    DB.SaveChanges();

                    if (ID_TipoLikert != null)
                    {
                       DB.InsertarAltTipoLikert(N_Cuestionario.Value, ID_Seccion.Value, ID_Pregunta.Value, ID_TipoLikert.Value);
                    }

                    //if (C_Secuencia.Length > 0)
                    //{
                    //    int nPreguntas = 0;
                    //    int nFinal = 0;
                    //    int nContador = 0;
                    //    string[] preguntas;
                    //    preguntas = C_Secuencia.Split(',');
                    //    nPreguntas = preguntas.Count();
                    //    foreach (string i in preguntas)
                    //    {
                    //        nContador = nContador + 1;
                    //        if (nContador == nPreguntas)
                    //        {
                    //            nFinal = 1;
                    //        }
                    //        res = DB.AgregarSecuencia(N_Cuestionario.Value, ID_Alternativa.Value, Convert.ToInt32(i), nFinal, C_Secuencia);
                    //    }
                    //}
                    ViewBag.N_Cuestionario = N_Cuestionario;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Configuracion", "Pregunta", new { N_Cuestionario = N_Cuestionario });
            }
        }

        [HttpPost]
        public ActionResult ConfiguracionAlternativa(int? N_Cuestionario, int? ID_Seccion, int? ID_Pregunta, int? ID_Alternativa, int? ID_Escala, int? N_AltComentario, bool? N_AltComentarioObligatorio, bool? N_FinalizaEncuesta, int? N_AltSecuencia, int? N_AltPregAnterior, int? N_AltPregSiguiente, string C_ComentarioEscala, string C_ComentariOblgEscala, bool? N_AltEmisionBoleto)
        {

            var tabla = new T_Config_Cuestionario();
            tabla = new T_Config_Cuestionario { ID_Cuestionario = N_Cuestionario.Value, ID_Seccion = ID_Seccion.Value, ID_Pregunta = ID_Pregunta.Value, ID_Alternativa = ID_Alternativa.Value };

            using (var DB = new CTX_Encuesta())
            {
                DB.T_Config_Cuestionario.Attach(tabla);
                
                tabla.N_FinalizaEncuesta = N_FinalizaEncuesta;
                tabla.N_AltPregAnterior = N_AltPregAnterior;
                if(N_AltComentario.Value == 1)
                {
                    tabla.N_AltComentario = true;
                }
                else
                {
                    tabla.N_AltComentario = false;
                }
                tabla.N_AltComentarioObligatorio = N_AltComentarioObligatorio;
                if (N_AltSecuencia.Value == 1)
                {
                    tabla.N_AltSecuencia = true;
                }
                else
                {
                    tabla.N_AltSecuencia = false;
                }
                tabla.N_AltPregSiguiente = N_AltPregSiguiente;
                tabla.N_AltEmisionBoleto = N_AltEmisionBoleto;
                tabla.A_ModificacionUsuario = TempData["Usuario"].ToString();
                tabla.A_ModificacionFecha = DateTime.Now;
                
                DB.Configuration.ValidateOnSaveEnabled = false;

                try
                {
                    DB.SaveChanges();

                    if (C_ComentarioEscala != "" || C_ComentariOblgEscala != "" )
                    {
                        DB.InsertarConfigEscala(ID_Alternativa.Value, ID_Pregunta.Value, C_ComentarioEscala, C_ComentariOblgEscala, TempData["Usuario"].ToString());
                    }

                    //if (C_Secuencia.Length > 0)
                    //{
                    //    int nPreguntas = 0;
                    //    int nFinal = 0;
                    //    int nContador = 0;
                    //    string[] preguntas;
                    //    preguntas = C_Secuencia.Split(',');
                    //    nPreguntas = preguntas.Count();
                    //    foreach (string i in preguntas)
                    //    {
                    //        nContador = nContador + 1;
                    //        if (nContador == nPreguntas)
                    //        {
                    //            nFinal = 1;
                    //        }
                    //        res = DB.AgregarSecuencia(N_Cuestionario.Value, ID_Alternativa.Value, Convert.ToInt32(i), nFinal, C_Secuencia);
                    //    }
                    //}
                    ViewBag.N_Cuestionario = N_Cuestionario;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Configuracion", "Pregunta", new { N_Cuestionario = N_Cuestionario });
            }
        }
        
        //public ActionResult Configuracion_datos(int? pCuestionario)
        //{
        //    if (!pCuestionario.HasValue) { pCuestionario = 0; }
        //    List<TMP_PreguntaAlter> lst = new List<TMP_PreguntaAlter>();
        //    string strSql = "SELECT ID_Cuestionario,ID_Pregunta,N_Orden,C_Pregunta, ID_Alternativa,C_Alternativa,B_TieneComentario,C_Secuencia,N_PreguntaSiguiente,B_FinalizaEncuesta,C_TipoPregunta"
        //            + " FROM T_Alternativa AS a"
        //            + " INNER JOIN T_Pregunta AS p ON a.N_Pregunta = p.ID_Pregunta"
        //            + " INNER JOIN T_TipoPregunta AS tp ON tp.ID_TipoPregunta = p.N_TipoPregunta"
        //            + " LEFT JOIN T_Seccion AS s ON s.ID_Seccion = p.N_Seccion"
        //            + " INNER JOIN T_Cuestionario AS c ON c.ID_Cuestionario = s.N_Cuestionario"
        //            + " WHERE ID_Cuestionario = " + pCuestionario
        //            + " ORDER BY N_Orden,ID_Alternativa";
        //    lst = DB.TMP_PreguntaAlter.SqlQuery(strSql).ToList();
        //    ViewBag.N_Cuestionario = pCuestionario.Value;
        //    return PartialView(lst);
        //}

        //public ActionResult Configuracion_edit(int ID_Alternativa)
        //{

        //    List<TMP_PreguntaAlter> lst = new List<TMP_PreguntaAlter>();

        //    string strSql = "SELECT ID_Cuestionario,ID_Pregunta,N_Orden,C_Pregunta, ID_Alternativa,C_Alternativa,ISNULL(B_TieneComentario,0) AS B_TieneComentario,"
        //            + " ISNULL(C_Secuencia,'') AS C_Secuencia,ISNULL(N_PreguntaSiguiente,0) AS N_PreguntaSiguiente,ISNULL(B_FinalizaEncuesta,0) AS B_FinalizaEncuesta,C_TipoPregunta"
        //            + " FROM T_Alternativa AS a"
        //            + " INNER JOIN T_Pregunta AS p ON a.N_Pregunta = p.ID_Pregunta"
        //            + " INNER JOIN T_TipoPregunta AS tp ON tp.ID_TipoPregunta = p.N_TipoPregunta"
        //            + " LEFT JOIN T_Seccion AS s ON s.ID_Seccion = p.N_Seccion"
        //            + " INNER JOIN T_Cuestionario AS c ON c.ID_Cuestionario = s.N_Cuestionario"
        //            + " WHERE ID_Alternativa = " + ID_Alternativa;

        //    lst = DB.TMP_PreguntaAlter.SqlQuery(strSql).ToList();

        //    ViewBag.N_Orden = lst.ElementAt(0).N_Orden;
        //    ViewBag.N_Cuestionario = lst.ElementAt(0).ID_Cuestionario;
        //    ViewBag.ID_Alternativa = lst.ElementAt(0).ID_Alternativa;
        //    ViewBag.C_Alternativa = lst.ElementAt(0).C_Alternativa;
        //    ViewBag.B_TieneComentario = lst.ElementAt(0).B_TieneComentario;
        //    ViewBag.C_Secuencia = lst.ElementAt(0).C_Secuencia;
        //    ViewBag.B_FinalizaEncuesta = lst.ElementAt(0).B_FinalizaEncuesta;

        //    return PartialView(lst);
        //}
    }
}
